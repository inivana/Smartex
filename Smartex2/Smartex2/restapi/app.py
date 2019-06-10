# -*- coding: utf8 -*-
from flask_httpauth import HTTPBasicAuth

from flask import Flask, Response, jsonify, request, abort

from flask import g
import sqlite3
import config
import hashlib
import datetime

"""
Error codes
0 - brak danych w bazie o podanym ID
1 - za ma³o danych do zrealizowania ¿¹dania
2 - zapytanie nie spowodowa³o ¿adnych zmian w bazie
"""

app = Flask(__name__)
auth = HTTPBasicAuth()


def dict_factory(cursor, row):
    d = {}
    for idx, col in enumerate(cursor.description):
        d[col[0]] = row[idx]
    return d


@auth.verify_password
def verify_password(username, password):
    print(password, str(hashlib.md5(password.encode("utf8")).hexdigest()))
    results = query_db("SELECT * FROM users WHERE login=? and password=?",
                       (username, hashlib.md5(password.encode("utf8")).hexdigest()))
    if len(results):
        return True
    return False


def get_db():
    db = getattr(g, "_database", None)
    if db is None:
        db = g._database = sqlite3.connect(config.DATABASE_PATH)

    db.row_factory = dict_factory
    return db


def query_db(query, args=(), one=False):
    cur = get_db().execute(query, args)
    rv = cur.fetchall()
    cur.close()
    return (rv[0] if rv else None) if one else rv


def execute_db(query, args=()):
    conn = get_db()
    cur = conn.execute(query, args)

    affected_rows = cur.rowcount

    conn.commit()
    cur.close()

    return affected_rows


@app.teardown_appcontext
def close_connection(exception):
    db = getattr(g, "_database", None)
    if db is not None:
        db.close()


@app.route("/")
def index():
    t = """
    <h1> Hi </h1>
    <ul>
    <li>/event/<b>id</b></li>
    <li>/user</li>
    <li>/post/<b>id</b></li>
    </ul>
    """
    return t


##############################
### Events
##############################


@app.route("/events/<int:user_id>", methods=["GET"])
@auth.login_required
def get_events(user_id):
    r = query_db("SELECT * FROM events where user_id=?", str(user_id))

    if len(r):
        return jsonify(status="success", result=r)
    else:
        return jsonify(status="failure", code=0)


@app.route("/events", methods=["POST"])
@auth.login_required
def add_event():
    if not {"user_id", "title", "start_date", "desc"}.issubset(request.json.keys()):
        return jsonify(status="failure", code=1, helper="Brakuje {}".format(
            {"user_id", "title", "start_date", "desc"}.difference(request.json.keys())))

    if execute_db("INSERT INTO events(user_id, title, start_date, desc, creation_date) VALUES (?, ?, ?, ?, ?)",
                  (request.json["user_id"],
                   request.json["title"],
                   request.json["start_date"],
                   request.json["desc"],
                   datetime.datetime.now())):
        return jsonify(status="success")

    return jsonify(status="failure", code=2)


@app.route("/events/<int:id>", methods=["PUT"])
@auth.login_required
def update_event(id):
    if execute_db("UPDATE events SET {}=? WHERE id=?".format("=?, ".join(request.json.keys())),
                  (*request.json.values(), str(id))):
        return jsonify(status="success")

    return jsonify(status="failure", code=2)


@app.route("/events/<int:id>", methods=["DELETE"])
@auth.login_required
def delete_event(id):
    if execute_db("DELETE FROM events WHERE id=?", str(id)):
        return jsonify(status="success")

    return jsonify(status="failure", code=2)


##############################
### Posts
##############################


# @app.route("/post/<int:id>")
# @auth.login_required
# def get_post(id):
#     r = query_db("SELECT * FROM posts where id=?", str(id))
#     return jsonify(r)f


@app.route("/posts/<int:event_id>", methods=["GET"])
@auth.login_required
def get_posts(event_id):
    r = query_db("SELECT * FROM posts where event_id=?", str(event_id))

    if len(r):
        return jsonify(status="success", result=r)
    else:
        return jsonify(status="failure", code=0)


@app.route("/posts", methods=["POST"])
@auth.login_required
def add_post():
    if not {"user_id", "event_id", "content"}.issubset(set(request.json.keys())):
        return jsonify(status="failure", code=1,
                       helper="Brakuje {}".format({"user_id", "event_id", "content"}.difference(request.json.keys())))

    if execute_db("INSERT INTO posts(user_id, event_id, content, creation_date) VALUES (?, ?, ?, ?)",
                  (request.json["user_id"],
                   request.json["event_id"],
                   request.json["content"],
                   datetime.datetime.now())):
        return jsonify(status="success")

    return jsonify(status="failure", code=2)


@app.route("/posts/<int:id>", methods=["PUT"])
@auth.login_required
def update_post(id):
    if execute_db("UPDATE posts SET {}=? WHERE id=?".format("=?, ".join(request.json.keys())),
                  (*request.json.values(), str(id))):
        return jsonify(success=True)

    return jsonify(status="failure", code=2)


@app.route("/posts/<int:id>", methods=["DELETE"])
@auth.login_required
def delete_post(id):
    if execute_db("DELETE FROM posts WHERE id=?", str(id)):
        return jsonify(status="success")

    return jsonify(status="failure", code=2)


##############################
### Users
##############################


@app.route("/user")
@auth.login_required
def get_logged_user_data():
    r = query_db("SELECT * FROM users where login=?", (auth.username(),), True)

    if len(r):
        return jsonify(status="success", result=r)
    else:
        return jsonify(status="failure", code=0)


@app.route("/user/<int:user_id>")
@auth.login_required
def get_user(user_id):
    r = query_db("SELECT * FROM users where id=?", (str(user_id),), True)

    if len(r):
        return jsonify(status="success", result=r)
    else:
        return jsonify(status="failure", code=0)


@app.route("/user", methods=["POST"])
@auth.login_required
def add_user():
    if not {"first_name",
            "last_name",
            "login",
            "password",
            "university",
            "faculty",
            "field_of_study"}.issubset(set(request.json.keys())):
        return jsonify(status="failure", code=1, helper="Brakuje {}".format({"first_name",
                                                                             "last_name",
                                                                             "login",
                                                                             "password",
                                                                             "university",
                                                                             "faculty",
                                                                             "field_of_study"}.difference(
            request.json.keys())))

    if execute_db("INSERT INTO users(first_name, last_name, login, password, university, faculty, field_of_study)"
                  " VALUES (?, ?, ?, ?, ?, ?, ?)",
                  (request.json["first_name"],
                   request.json["last_name"],
                   request.json["login"],
                   request.json["password"],
                   request.json["university"],
                   request.json["faculty"],
                   request.json["field_of_study"])):
        return jsonify(status="success")

    return jsonify(status="failure", code=2)

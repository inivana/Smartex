using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex2.Model
{
    class Post
    {
        private int _id;
        private int _userId;
        private int _eventId;
        private string _content;
        private DateTime _creationDateTime;



        //properties
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public int EventId
        {
            get { return _eventId; }
            set { _eventId = value; }
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public DateTime CreationDateTime
        {
            get { return _creationDateTime; }
            set { _creationDateTime = value; }
        }

    }
}

using SQLite;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Smartex.Model
{
    class GradeBook
    {
        #region fields   

        private ObservableCollection<Subject> _subjects;

        public ObservableCollection<Subject> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        #endregion

        #region database

        public static bool Add(Subject subject)
        {
            int rows;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Subject>();
                rows = conn.Insert(subject);
            }

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ObservableCollection<Subject> GetSubjects()
        {
            int rows;
            ObservableCollection<Subject> subjects;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Subject>();
                subjects = new ObservableCollection<Subject>(conn.Table<Subject>().ToList());
            }
            return subjects;
        }

        #endregion

        #region database
        public static ObservableCollection<Grade> GetGrades(int subjectID)
        {
            int rows;
            ObservableCollection<Grade> grades;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Grade>();
                grades = new ObservableCollection<Grade>(conn.Table<Grade>().Where(g => g.SubjectId == subjectID).ToList());
            }
            return grades;
        }

        public static bool AddGrade(Grade grade)
        {
            int rows;
            ObservableCollection<Grade> grades;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Grade>();
                rows = conn.Insert(grade);
            }

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> DeleteGrade(Grade grade)
        {
            var yesSelected = await App.Current.MainPage.DisplayAlert("Usuń ocenę", "Czy chcesz usunąć tę ocenę?", "Tak", "Nie");
            if (yesSelected)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Grade>();
                    int result = conn.Delete(grade);
                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}

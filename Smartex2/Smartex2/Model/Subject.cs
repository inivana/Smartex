using Smartex.Annotations;
using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Smartex.Model
{
    public class Subject : INotifyPropertyChanged
    {
        #region fields

        private string _name;
        private ObservableCollection<Grade> _grades;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Ignore] //w sqlite nie chcemy tej kolumny
        public ObservableCollection<Grade> Grades
        {
            get { return _grades; }
            set
            {
                _grades = value;
                OnPropertyChanged("Grades");
            }
        }

        [MaxLength(250)]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value; 
                OnPropertyChanged("Name");
            }
        }

        #endregion

        #region binding

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

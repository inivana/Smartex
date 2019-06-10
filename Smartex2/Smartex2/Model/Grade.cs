using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Smartex.Annotations;
using SQLite;

namespace Smartex.Model
{
    public class Grade : INotifyPropertyChanged
    {
        private int _grade;
        private string _description;

        public int SubjectId { get; set; }

        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public int IntGrade
        {
            //ocena
            get { return _grade; }
            set
            {
                _grade = value;
                OnPropertyChanged("IntGrade");
            }
        }

        [MaxLength(250)]
        public string Description
        {
            //np. laborka ze swinga
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

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

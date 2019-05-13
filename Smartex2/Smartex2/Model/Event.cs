using System;

namespace Smartex2.Model
{
    public class Event
    {
        private int _id;
        private int _userId;
        private string _title;
        private DateTime _startDateTime;
        private string _description;
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

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public DateTime StartDateTime
        {
            get { return _startDateTime; }
            set { _startDateTime = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public DateTime CreationDateTime
        {
            get { return _creationDateTime; }
            set { _creationDateTime = value; }
        }

    }
}

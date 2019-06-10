using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex.Model
{
    public enum MenuItemType
    {
        HomePage,
        Profile,
        GradeBook,
        Logout
    }
    class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}

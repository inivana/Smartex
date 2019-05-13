using System;
using System.Collections.Generic;
using System.Text;

namespace Smartex2.Model
{
    public enum MenuItemType
    {
        HomePage,
        Profile,
        Calendar,
        GradeBook,
        Settings,
        Logout
    }
    class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}


using System.Collections.Generic;
using toyrobot.Enums;

namespace toyrobot.web.Models
{
    /// <summary>
    /// Model used to provide values on the landing page.
    /// </summary>
    public class HomePageViewModel
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int BoxWidth { get; set; }
        public int BoxHeight { get; set; }
        public IEnumerable<Face> Directions { get; set; }
    }
}
using System.Collections.Generic;

namespace TimesheetApp.Models
{
    public class MeObject
    {
        public int personid { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public int listcount { get; set; }
        public List<Me> me { get; set; }
    }

    public class Me
    {
        public string id { get; set; }
        public string localid { get; set; }
        public string clientid { get; set; }
        public string title { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string primaryaccount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetApp.Models
{
    public class ProjectModuleObject
    {
        public int personid { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public int listcount { get; set; }
        public List<Projectmodule> projectmodule { get; set; }
    }

    public class Projectmodule
    {
        public string id { get; set; }
        public string projectid { get; set; }
        public string moduleid { get; set; }
        public string active { get; set; }
        public string modulename { get; set; }
        public string module { get; set; }
    }
}

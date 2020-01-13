using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetApp.Models
{
    public class ProjectWorkTypeObject
    {
        public int personid { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public int listcount { get; set; }
        public List<Projectworktype> projectworktype { get; set; }
    }

    public class Projectworktype
    {
        public string id { get; set; }
        public string projectid { get; set; }
        public string worktypeid { get; set; }
        public string worktype { get; set; }
        public string active { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetApp.Models
{
    public class TimeObject
    {
        public string personid { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string projectid { get; set; }
        public string moduleid { get; set; }
        public string worktypeid { get; set; }
        public string billable { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetApp.Models
{
    public class TimeResponseObject
    {
        public int personid { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public Time time { get; set; }
    }

    public class Time
    {
        public string id { get; set; }
        public string projectid { get; set; }
        public string moduleid { get; set; }
        public object taskid { get; set; }
        public string worktypeid { get; set; }
        public string personid { get; set; }
        public string date { get; set; }
        public string datemodified { get; set; }
        public string time { get; set; }
        public string description { get; set; }
        public string billable { get; set; }
        public string worktype { get; set; }
        public object milestoneid { get; set; }
        public object ogmilestoneid { get; set; }
    }
}

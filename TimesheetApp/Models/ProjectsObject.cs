using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetApp.Models
{
    public class ProjectsObject
    {
        public int personid { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public int listcount { get; set; }
        public List<Project> project { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string datestart { get; set; }
        public object dateend { get; set; }
        public string alert_percent { get; set; }
        public object alert_date { get; set; }
        public string active { get; set; }
        public string billable { get; set; }
        public object budget { get; set; }
        public string clientid { get; set; }
        public string client { get; set; }
        public string clientlocalid { get; set; }
        public string localidunpadded { get; set; }
        public string localid { get; set; }
        public string manager { get; set; }
        public string managerid { get; set; }
    }
}

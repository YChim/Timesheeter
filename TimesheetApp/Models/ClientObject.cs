using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetApp.Models
{
    public class ClientObject
    {
        public int personid { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public Client client { get; set; }
    }

    public class Client
    {
        public string id { get; set; }
        public string localid { get; set; }
        public string localidunpadded { get; set; }
        public string name { get; set; }
        public string active { get; set; }
    }
}

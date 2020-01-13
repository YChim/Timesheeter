using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetApp.Models
{
    public class UserData
    {
        public string PersonId { get; }
        public string Token { get; }
        public string Client { get; }
        public string ClientId { get; }
        public string Project { get; }
        public string ProjectId { get; }
        public string Module { get; }
        public string ModuleId { get; }
        public string WorkType { get; }
        public string WorkTypeId { get; }

        public UserData(string personId,
            string token,
            string client,
            string clientId,
            string project,
            string projectId,
            string module,
            string moduleId,
            string workType,
            string workTypeId)
        {
            PersonId = personId;
            Token = token;
            Client = client;
            ClientId = clientId;
            Project = project;
            ProjectId = projectId;
            Module = module;
            ModuleId = moduleId;
            WorkType = workType;
            WorkTypeId = workTypeId;
        }
    }
}

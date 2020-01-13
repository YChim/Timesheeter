using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TimesheetApp.Helpers;
using TimesheetApp.Models;

namespace TimesheetApp.Logic
{
    public class PrimaryInformation
    {
        private readonly TimeTaskService _timeTaskService;

        public PrimaryInformation(TimeTaskService timeTaskService)
        {
            _timeTaskService = timeTaskService;
        }

        public async Task<bool> GetMyInformation()
        {
            try
            {
                if (await GetUserInformation() == false)
                    return false;

                if (await GetClient() == false)
                    return false;

                if (await GetProjectInformation() == false)
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return true;
        }

        private async Task<bool> GetUserInformation()
        {
            //User Information
            var uri = _timeTaskService.BaseUri + "me";
            var result = await _timeTaskService.MakeServiceCall(uri, RequestType.Get);
            if (string.IsNullOrEmpty(result))
                return false;
            Me = JsonConvert.DeserializeObject<MeObject>(result);
            return true;
        }

        private async Task<bool> GetClient()
        {
            //Client Information
            var uri = _timeTaskService.BaseUri + "client/" + Me.me.FirstOrDefault().clientid;
            var result = await _timeTaskService.MakeServiceCall(uri, RequestType.Get);
            if (string.IsNullOrEmpty(result))
                return false;
            Client = JsonConvert.DeserializeObject<ClientObject>(result);
            return true;
        }

        private async Task<bool> GetProjectInformation()
        {
            //Project Information
            var uri = _timeTaskService.BaseUri + "project/?clientid=" + Me.me.FirstOrDefault().clientid;
            var result = await _timeTaskService.MakeServiceCall(uri, RequestType.Get);
            if (string.IsNullOrEmpty(result))
                return false;
            Projects = JsonConvert.DeserializeObject<ProjectsObject>(result);
            return true;
        }

        public async Task<ProjectModuleObject> GetProjectModules(Project project)
        {
            //Selected Project Modules
            var uri = _timeTaskService.BaseUri + "projectmodule/?projectid=" + project.id;
            var result = await _timeTaskService.MakeServiceCall(uri, RequestType.Get);
            return JsonConvert.DeserializeObject<ProjectModuleObject>(result);
        }

        public async Task<ProjectWorkTypeObject> GetProjectWorkTypes(Project project)
        {
            //Selected Project WorkTypes
            var uri = _timeTaskService.BaseUri + "projectworktype/?projectid=" + project.id;
            var result = await _timeTaskService.MakeServiceCall(uri, RequestType.Get);
            return JsonConvert.DeserializeObject<ProjectWorkTypeObject>(result);
        }

        public MeObject Me { get; private set; }
        public ProjectsObject Projects { get; private set; }
        public ClientObject Client { get; private set; }
    }
}

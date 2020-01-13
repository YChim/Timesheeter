using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TimesheetApp.Helpers;
using TimesheetApp.Models;

namespace TimesheetApp.Logic
{
    public class UserTimes
    {
        private readonly TimeTaskService _timeTaskService;
        private readonly List<TimeObject> _times;

        public UserTimes(TimeTaskService timeTaskService,
            List<TimeObject> times)
        {
            _timeTaskService = timeTaskService;
            _times = times;
        }

        public async Task<string> SubmitAllTimes()
        {
            var uri = _timeTaskService.BaseUri + "time/";
            var errors = string.Empty;

            foreach (var time in _times)
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(time), Encoding.UTF8, "application/json");
                var message = await _timeTaskService.MakeServiceCall(uri, RequestType.Post, stringContent);
                var timeResponse = JsonConvert.DeserializeObject<TimeResponseObject>(message);
                if (timeResponse == null)
                {
                    errors += " " + time.date;
                }
            }

            return errors;
        }
    }
}

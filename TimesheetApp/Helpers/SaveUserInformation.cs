using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetApp.Models;

namespace TimesheetApp.Helpers
{
    public class SaveUserInformation
    {
        public static void SaveUserInformationToCSV(
            string personId,
            string token,
            string client,
            string clientId,
            string project,
            string projectId,
            string module,
            string moduleId,
            string worktype,
            string worktypeId)
        {
            try
            {
                var csv = new StringBuilder();

                var newLine =
                    $"{personId},{token},{client},{clientId},{project},{projectId},{module},{moduleId},{worktype},{worktypeId}";
                csv.AppendLine(newLine);

                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "localdata.csv", csv.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

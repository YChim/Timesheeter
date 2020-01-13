using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetApp.Models;

namespace TimesheetApp.Helpers
{
    public class ReadUserData
    {
        public static UserData ReadUserDataFromCsv()
        {
            UserData userData = null;

            try
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "localdata.csv"))
                {
                    using (var reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "localdata.csv"))
                    {
                        var line = reader.ReadLine();
                        if (line == null) return null;
                        var values = line.Split(',');
                        userData = new UserData(values[0],
                            values[1],
                            values[2],
                            values[3],
                            values[4],
                            values[5],
                            values[6],
                            values[7],
                            values[8],
                            values[9]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return userData;
        }
    }
}

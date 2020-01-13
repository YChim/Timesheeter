using System;
using System.Collections.Generic;
using System.IO;

namespace TimesheetApp.Helpers
{
    public class ReadCSVFile
    {
        public static Dictionary<string, double> GetTimesPerDay(
            Stream stream, 
            int day, 
            int hours)
        {
            var times = new Dictionary<string, double>();

            using (var sr = new StreamReader(stream))
            {
                string row;
                var isFirstRow = true;
                while ((row = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(row)) continue;
                    if (!isFirstRow)
                    {
                        var list = row.Split(',');
                        if (times.ContainsKey(list[day].ToLower()))
                        {
                            times[list[day]] += ConvertToDecimalTime(list[hours]);
                        }
                        else
                        {
                            if (Math.Abs(ConvertToDecimalTime(list[hours])) > 0.0)
                            {
                                times.Add(list[day].ToLower(), ConvertToDecimalTime(list[hours]));
                            }
                        }
                    }
                    else
                    {
                        isFirstRow = false;
                    }
                }
            }
            return times;
        }

        private static double ConvertToDecimalTime(string hours)
        {
            var h = hours.Split(':');
            var decimalHours = double.Parse(h[0]) + double.Parse(h[1]) / 60;
            return decimalHours;
        }
    }
}

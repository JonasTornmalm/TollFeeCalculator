using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class FileReader
    {
        public List<DateTime> DateTimeParser(string dateString)
        {
            string[] dateStrings = dateString.Split(", ");
            DateTime[] tollDateTimes = new DateTime[dateStrings.Length];
            for (int i = 0; i < dateStrings.Length; i++)
            {
                try
                {
                    tollDateTimes[i] = DateTime.Parse(dateStrings[i]);
                }
                catch (Exception)
                {
                    tollDateTimes[i] = DateTime.MinValue;
                }
            }
            return tollDateTimes.ToList();
        }

        public string ReadFileFromDirectory(string fileToRead)
        {
            try
            {
                return File.ReadAllText(fileToRead);
            }
            catch (Exception)
            {
                throw new DirectoryNotFoundException();
            }
        }
    }
}

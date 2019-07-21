using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TGUWeb.Models.WebApiModels
{
    public class PatientVideo : IComparable<PatientVideo>
    {
        public string FileName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public PatientVideo(string fileName, string date, string time)
        {
            FileName = fileName;
            Date = date;
            Time = time;
        }

        public int CompareTo(PatientVideo other)
        {
            int thisId = int.Parse(FileName.Substring(3, FileName.Length - 7));
            int otherId = int.Parse(other.FileName.Substring(3, other.FileName.Length - 7));

            if (thisId < otherId)
                return -1;
            else if (thisId > otherId)
                return 1;
            else
                return 0;
        }
    }
}
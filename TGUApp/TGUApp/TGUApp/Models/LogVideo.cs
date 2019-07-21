using System;
using System.Collections.Generic;
using System.Text;

using TGUApp.Enumerations;

namespace TGUApp.Models
{
    public class LogVideo : IComparable<LogVideo>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public UploadStatus UploadStatus { get; set; }

        public LogVideo()
        { }

        public LogVideo(int id, string fileName, string dateOf, string timeOf, UploadStatus uploadStatus)
        {
            Id = id;
            FileName = fileName;

            Date = dateOf;
            Time = timeOf;

            UploadStatus = uploadStatus;
        }

        public int CompareTo(LogVideo other)
        {
            if (Id < other.Id)
                return -1;
            else if (Id > other.Id)
                return 1;
            else
                return 0;
        }
    }
}

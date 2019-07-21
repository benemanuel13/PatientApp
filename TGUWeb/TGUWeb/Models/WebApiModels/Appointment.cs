using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TGUWeb.Models.WebApiModels
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DiaryId { get; set; }
        
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Time { get; set; }
        public string WhoWith { get; set; }
        public string Where { get; set; }

        public Appointment()
        { }

        public Appointment(int id, int diaryId, int day, int month, int year, string time, string whoWith, string where)
        {
            Id = id;
            DiaryId = diaryId;
            
            Day = day;
            Month = month;
            Year = year;
            Time = time;

            WhoWith = whoWith;
            Where = where;
        }
    }
}
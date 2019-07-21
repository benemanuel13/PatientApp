using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Models
{
    public class Diary
    {
        public int Id { get; set; }

        private List<Appointment> appointments = new List<Appointment>();

        public Diary()
        { }

        public Diary(int id)
        {
            Id = id;
        }

        public List<Appointment> Appointments
        {
            get
            {
                return appointments;
            }

            set
            {
                appointments = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Models
{
    public class ServiceUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NHSNumber { get; set; }
        public bool UnderFOLS { get; set; }

        public ServiceUser()
        {
        }

        public ServiceUser(int id, string name, string nhsNumber, bool underFOLS)
        {
            Id = id;
            Name = name;
            NHSNumber = nhsNumber;
            UnderFOLS = underFOLS;
        }
    }
}

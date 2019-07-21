using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TGUWeb.Models.WebApiModels
{
    public class CarePlan
    {
        public int Id { get; set; }
        public List<Challenge> Challenges { get; set; } = new List<Challenge>();

        public CarePlan()
        { }

        public CarePlan(int id)
        {
            Id = id;
        }
    }
}
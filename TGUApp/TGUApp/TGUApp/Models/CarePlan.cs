using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Models
{
    public class CarePlan
    {
        public int Id { get; set; }
        public List<Challenge> challenges = new List<Challenge>();

        public CarePlan()
        { }

        public CarePlan(int id)
        {
            Id = id;
        }

        public List<Challenge> Challenges
        {
            get
            {
                return challenges;
            }

            set
            {
                challenges = value;
            }
        }
    }
}

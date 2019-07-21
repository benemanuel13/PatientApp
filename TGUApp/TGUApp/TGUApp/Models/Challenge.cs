using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public int CarePlanId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }

        private List<Intervention> interventions = new List<Intervention>();

        public Challenge()
        { }

        public Challenge(int id, int carePlanId, string title, string category, string subCategory)
        {
            Id = id;
            CarePlanId = carePlanId;
            Title = title;
            Category = category;
            SubCategory = subCategory;
        }

        public List<Intervention> Interventions
        {
            get
            {
                return interventions;
            }

            set
            {
                interventions = value;
            }
        }
    }
}

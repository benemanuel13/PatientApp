using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Models
{
    public class Intervention
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public bool ClientAgreed { get; set; }
        public bool Section117 { get; set; }

        private List<Goal> goals = new List<Goal>();
        private List<Activity> activities = new List<Activity>();

        public string ClientsView { get; set; }

        public Intervention()
        { }

        public Intervention(int id, string category, string type, bool clientAgreed, bool section, string clientsView)
        {
            Id = id;
            Category = category;
            Type = type;
            ClientAgreed = clientAgreed;
            Section117 = section;
            ClientsView = clientsView;
        }

        public List<Goal> Goals
        {
            get
            {
                return goals;
            }

            set
            {
                goals = value;
            }
        }

        public List<Activity> Activities
        {
            get
            {
                return activities;
            }

            set
            {
                activities = value;
            }
        }
    }
}

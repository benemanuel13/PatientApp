using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public int InterventionId { get; set; }
        public string Text { get; set; }

        public Activity()
        { }

        public Activity(int id, int interventionId, string text)
        {
            Id = id;
            InterventionId = interventionId;
            Text = text;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TGUWeb.Models.WebApiModels
{
    public class Goal
    {
        public int Id { get; set; }
        public int InterventionId { get; set; }
        public string Text { get; set; }

        public Goal()
        { }

        public Goal(int id, int interventionId, string text)
        {
            Id = id;
            InterventionId = interventionId;
            Text = text;
        }
    }
}
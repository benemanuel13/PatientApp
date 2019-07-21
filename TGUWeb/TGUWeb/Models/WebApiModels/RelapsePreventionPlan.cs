using System;
using System.Collections.Generic;
using System.Text;

namespace TGUWeb.Models.WebApiModels
{
    public class RelapsePreventionPlan
    {
        int Id { get; set; }

        public RelapsePreventionPlan()
        { }

        public RelapsePreventionPlan(int id)
        {
            Id = id;
        }
    }
}

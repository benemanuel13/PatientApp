using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Models
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

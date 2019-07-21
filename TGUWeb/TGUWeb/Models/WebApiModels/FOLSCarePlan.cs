using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TGUWeb.Models.WebApiModels
{
    public class FOLSCarePlan
    {
        public int Id { get; set; }

        public FOLSCarePlan()
        { }

        public FOLSCarePlan(int id)
        {
            Id = id;
        }
    }
}
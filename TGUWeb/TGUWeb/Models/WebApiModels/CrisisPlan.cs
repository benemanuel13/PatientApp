using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TGUWeb.Models.WebApiModels
{
    public class CrisisPlan
    {
        int Id { get; set; }

        private List<CrisisPlanSection> sections = new List<CrisisPlanSection>();

        public CrisisPlan()
        { }

        public CrisisPlan(int id)
        {
            Id = id;
        }

        public List<CrisisPlanSection> Sections
        {
            get
            {
                return sections;
            }

            set
            {
                sections = value;
            }
        }
    }
}
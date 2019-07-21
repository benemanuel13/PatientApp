using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Models
{
    public class CrisisPlanSection
    {
        public int Id { get; set; }
        public int CrisisPlanId { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }

        public CrisisPlanSection()
        { }

        public CrisisPlanSection(int id, int crisisPlanId, string heading, string text)
        {
            Id = id;
            CrisisPlanId = crisisPlanId;
            Heading = heading;
            Text = text;
        }
    }
}

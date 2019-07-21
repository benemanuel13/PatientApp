using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Models
{
    public class Language
    {
        public string LangCode { get; set; }
        public string Name { get; set; }

        public Language(string langCode, string name)
        {
            LangCode = langCode;
            Name = name;
        }
    }
}

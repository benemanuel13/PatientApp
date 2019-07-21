using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Presentation.EventArgs
{
    public class LanguageSelectedEventArgs : System.EventArgs
    {
        public string LangCode { get; set; }

        public LanguageSelectedEventArgs(string langCode)
        {
            LangCode = langCode;
        }
    }
}

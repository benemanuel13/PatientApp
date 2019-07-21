using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BensTranslatorWin
{
    public class TextTranslation
    {
        public string LangCode { get; set; }
        //public string LangCodeFrom { get; set; }
        public string Text { get; set; }

        public TextTranslation()
        { }

        public TextTranslation(string langCode, string text)
        {
            LangCode = langCode;
            //LangCodeFrom = langCodeFrom;
            Text = text;
        }
    }
}
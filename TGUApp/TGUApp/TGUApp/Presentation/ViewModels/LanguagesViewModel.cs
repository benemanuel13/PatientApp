using System;
using System.Collections.Generic;
using System.Text;

using TGUApp.Models;

namespace TGUApp.Presentation.ViewModels
{
    public class LanguagesViewModel
    {
        public List<Language> Languages = new List<Language>();

        public LanguagesViewModel()
        {
            Languages.Add(new Language("en", "English"));
            Languages.Add(new Language("el", "Greek"));
            Languages.Add(new Language("fr", "French"));
            Languages.Add(new Language("de", "German"));
        }
    }
}

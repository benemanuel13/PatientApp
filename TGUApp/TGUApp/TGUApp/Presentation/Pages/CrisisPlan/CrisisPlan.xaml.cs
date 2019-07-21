using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Models;
using TGUApp.Infrastructure;
using TGUApp.Presentation.Views.CrisisPlan;

namespace TGUApp.Presentation.Pages.CrisisPlan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrisisPlan : ContentPage
    {
        public CrisisPlan()
        {
            InitializeComponent();

            ServiceUser user = (Internet.GetServiceUser(1)).Result;

            if (!user.UnderFOLS)
            {
                Models.CrisisPlan plan = (Internet.GetCrisisPlan(user.Id)).Result;

                foreach (Models.CrisisPlanSection section in plan.Sections)
                    Sections.Children.Add(new Views.CrisisPlan.CrisisPlanSection(section));
            }
        }
    }
}
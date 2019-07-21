using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGUApp.Presentation.Views.CrisisPlan
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CrisisPlanSection : ContentView
	{
        public CrisisPlanSection(Models.CrisisPlanSection section)
        {
            InitializeComponent();

            Heading.Text = section.Heading;

            section.Text = section.Text.Replace("\\r\\n", "\r\n");

            Text.Text = section.Text;
        }
    }
}
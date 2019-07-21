using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGUApp.Presentation.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Heading : ContentView
	{
		public Heading ()
		{
			InitializeComponent ();
		}

        public string MainTitle
        {
            set
            {
                HeadingTitle.Text = value;
            }
        }
	}
}
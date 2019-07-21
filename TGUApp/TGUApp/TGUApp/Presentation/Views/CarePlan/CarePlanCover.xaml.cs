using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Models;
using TGUApp.Infrastructure;

namespace TGUApp.Presentation.Views.CarePlan
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarePlanCover : ContentView
	{
		public CarePlanCover (ServiceUser user)
		{
			InitializeComponent ();

            NHSNumber.Text = user.NHSNumber;
		}
	}
}
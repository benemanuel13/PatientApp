using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Infrastructure;
using TGUApp.Models;

namespace TGUApp.Presentation.Pages.RelapsePreventionPlan
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RelapsePreventionPlan : ContentPage
	{
		public RelapsePreventionPlan ()
		{
			InitializeComponent ();

            ServiceUser user = (Internet.GetServiceUser(1)).Result;

            Models.RelapsePreventionPlan plan = (Internet.GetRelapsePreventionPlan(user.Id)).Result;


        }
	}
}
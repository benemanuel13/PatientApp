using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Models;
using TGUApp.Infrastructure;
using TGUApp.Presentation.Views.CarePlan;

namespace TGUApp.Presentation.Pages.CarePlan
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarePlanHome : ContentPage
	{
        private List<View> Views { get; } = new List<View>();

        public CarePlanHome ()
		{
			InitializeComponent ();

            ServiceUser user = (Internet.GetServiceUser(1)).Result;

            if (!user.UnderFOLS)
            {
                Views.Add(new CarePlanCover(user));

                Models.CarePlan plan = (Internet.GetCarePlan(user.Id)).Result;

                foreach (Challenge challenge in plan.Challenges)
                    Views.Add(new CarePlanChallengesAndNeeds(challenge));

                PlanViews.Position = 0;
                PlanViews.ItemsSource = Views;
            }
        }

        public string MainTitle
        {
            set
            {
                Heading.MainTitle = value;
            }
        }
	}
}
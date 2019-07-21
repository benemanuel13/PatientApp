using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Models;

namespace TGUApp.Presentation.Views.CarePlan
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarePlanChallengesAndNeeds : ContentView
	{
        public CarePlanChallengesAndNeeds()
        {
            InitializeComponent();
        }

        public CarePlanChallengesAndNeeds(Challenge challenge)
        {
            InitializeComponent();

            Title.Text = challenge.Title;
            Category.Text = challenge.Category;
            SubCategory.Text = challenge.SubCategory;

            foreach (Models.Intervention intervention in challenge.Interventions)
                Interventions.Children.Add(new Intervention(intervention));
        }
    }
}
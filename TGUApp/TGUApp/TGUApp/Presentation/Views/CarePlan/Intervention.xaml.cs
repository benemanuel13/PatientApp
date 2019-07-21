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
	public partial class Intervention : ContentView
	{
		public Intervention ()
		{
            InitializeComponent();
        }

        public Intervention(Models.Intervention intervention)
        {
            InitializeComponent();

            InterventionCategory.Text = intervention.Category;
            InterventionType.Text = intervention.Type;

            if (intervention.ClientAgreed)
                ClientAgreed.Text = "Yes";
            else
                ClientAgreed.Text = "No";

            if (intervention.Section117)
                Section117.Text = "Yes";
            else
                Section117.Text = "No";

            StringBuilder goals = new StringBuilder();

            foreach (Goal goal in intervention.Goals)
            {
                goals.Append(goal.Text);
                goals.Append("\r\n");
            }

            string goalsText = goals.ToString();
            goalsText = goalsText.Substring(0, goalsText.Length - 2);

            Goals.Text = goalsText;

            StringBuilder activities = new StringBuilder();

            foreach (Activity activity in intervention.Activities)
            {
                activities.Append(activity.Text);
                activities.Append("\r\n");
            }

            string activitiesText = activities.ToString();
            activitiesText = activitiesText.Substring(0, activitiesText.Length - 2);

            Activities.Text = activitiesText;

            intervention.ClientsView = intervention.ClientsView.Replace("\\r", "\r");
            intervention.ClientsView = intervention.ClientsView.Replace("\\n", "\n");

            ClientsView.Text = intervention.ClientsView;
        }
	}
}
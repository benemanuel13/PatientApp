using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGUApp.Presentation.Views.AppointmentDiary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppointmentSection : ContentView
	{
		public AppointmentSection ()
		{
            InitializeComponent();
        }

        public AppointmentSection(Models.Appointment appointment)
        {
            InitializeComponent();

            Date.Text = appointment.Day + "/" + appointment.Month + "/" + appointment.Year + ", " + appointment.Time;
            WhoWith.Text = appointment.WhoWith;
            Where.Text = appointment.Where;
        }
    }
}
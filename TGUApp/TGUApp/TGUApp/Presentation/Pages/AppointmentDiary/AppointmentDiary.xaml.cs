using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Models;
using TGUApp.Infrastructure;

namespace TGUApp.Presentation.Pages.AppointmentDiary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppointmentDiary : ContentPage
	{
		public AppointmentDiary ()
		{
			InitializeComponent ();

            ServiceUser user = (Internet.GetServiceUser(1)).Result;

            Diary diary = (Internet.GetDiary(1)).Result;

            foreach (Appointment appointment in diary.Appointments)
            {
                Appointments.Children.Add(new Views.AppointmentDiary.AppointmentSection(appointment));
            }
        }
	}
}
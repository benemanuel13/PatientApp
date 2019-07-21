using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Presentation.ViewModels;
using TGUApp.Presentation.Pages.CarePlan;

namespace TGUApp.Presentation.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        ViewModels.HomePageViewModel viewModel;

		public HomePage (Presentation.ViewModels.HomePageViewModel model)
		{
			InitializeComponent ();

            viewModel = model;

            this.BindingContext = viewModel;

            //NavigationCommand = new Command(NavigationCommandToInfo);
            //ToolbarItems.Clear();
            //ToolbarItems.Add(new ToolbarItem() { Text = "Test", Icon = "logoicon.png", Command = NavigationCommand });
        }

        public Command NavigationCommand { get; }

        void NavigationCommandToInfo()
        {
            //Navigation.PushAsync(new RecepieDetails());
        }

        private void CarePlanButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CarePlanHome());
        }

        private void CrisisButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CrisisPlan.CrisisPlan());
        }

        private void RelapsePrevButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new RelapsePreventionPlan.RelapsePreventionPlan());
        }

        private void DiaryButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AppointmentDiary.AppointmentDiary());
        }

        private void VideoLogButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VideoLog.VideoLogHome());
        }

        private void TeamContactButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new TeamContact.TeamContact());
        }

        private void EmergencyButton_Clicked(object sender, System.EventArgs e)
        {

        }

        private void InterventionButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new InterventionVideos.InterventionVideos());
        }

        private void NavBar_MenuTapped(object sender, EventArgs.MenuTappedEventArgs e)
        {
            LanguagesViewModel model = new LanguagesViewModel();

            LanguagePicker.LanguagePicker picker = new LanguagePicker.LanguagePicker(model);
            picker.LanguageSelected += Picker_LanguageSelected;

            Navigation.PushAsync(picker);
        }

        private void Picker_LanguageSelected(object sender, EventArgs.LanguageSelectedEventArgs e)
        {
            App.LangCode = e.LangCode;

            viewModel = new VMGenerator<HomePageViewModel>().CreateView(e.LangCode);

            this.BindingContext = viewModel;
        }
    }
}
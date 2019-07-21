using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Infrastructure;
using TGUApp.Interfaces;
using TGUApp.Presentation.Pages;

using TGUApp.Presentation.ViewModels;
using TGUApp.Presentation.Views.VideoLog;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TGUApp
{
    public partial class App : Application
    {
        public static string LangCode { get; set; } = "en";

        public static IDatabase Database { get; set; }

        public static Dictionary<int, VideoEntry> VideoEntries = new Dictionary<int, VideoEntry>();

        public App()
        {
            InitializeComponent();

            SplashScreen splash = new SplashScreen();
            splash.Finished += Splash_Finished;

            MainPage = splash;

            splash.Start();
        }

        private void Splash_Finished(object sender, Presentation.EventArgs.SplashScreenFinishedEventArgs e)
        {
            Database = DependencyService.Get<IDatabase>();
            //Database = new Database();

            MainPage = new NavigationPage(new HomePage(new VMGenerator<HomePageViewModel>().CreateView(LangCode)));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

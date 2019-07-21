using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Threading;

using TGUApp.Presentation.EventArgs;

namespace TGUApp.Presentation.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        public event EventHandler<SplashScreenFinishedEventArgs> Finished;

        public SplashScreen()
        {
            InitializeComponent();
        }

        public async void Start()
        {
            await Task.Run(() => Thread.Sleep(3000));

            Finished(this, new SplashScreenFinishedEventArgs());
        }
    }
}
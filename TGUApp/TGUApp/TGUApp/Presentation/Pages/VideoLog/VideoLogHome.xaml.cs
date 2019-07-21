using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGUApp.Presentation.Pages.VideoLog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoLogHome : ContentPage
    {
        public VideoLogHome()
        {
            InitializeComponent();
        }

        private void RecordButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VideoLog(1));
        }

        private void RecordBackButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VideoLog(0));
        }

        private void ViewButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VideoLogPicker());
        }
    }
}
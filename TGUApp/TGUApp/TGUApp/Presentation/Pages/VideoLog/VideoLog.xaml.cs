using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.IO;

using TGUApp.Interfaces;

namespace TGUApp.Presentation.Pages.VideoLog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VideoLog : ContentPage
	{
        private ICamera camera;
        int currentCamera = 1;
        int id = 0;
        string filename = "";

        public VideoLog (int camera)
		{
			InitializeComponent ();
            currentCamera = camera;
		}

        private void StartRecording_Clicked(object sender, System.EventArgs e)
        {
            StartRecording.IsEnabled = false;
            StopRecording.IsEnabled = true;

            camera = DependencyService.Get<ICamera>();

            id = App.Database.GetNewVideoId();
            string filename = "Ben" + id.ToString() + ".mp4";

            camera.FileName = filename;
            bool frontFacing = camera.IsFrontCamera(currentCamera);

            VideoElement.Renderer.SetSurface(frontFacing);

            camera.Surface = VideoElement.Surface;
            camera.StartRecording(currentCamera, true);
        }

        private void StopRecording_Clicked(object sender, System.EventArgs e)
        {
            StartRecording.IsEnabled = true;
            StopRecording.IsEnabled = false;

            camera.StopRecording();

            App.Database.AddVideo(id, filename);
        }

        private void Switch_Clicked(object sender, System.EventArgs e)
        {
            currentCamera = camera.SwitchCamera(currentCamera);
        }
    }
}
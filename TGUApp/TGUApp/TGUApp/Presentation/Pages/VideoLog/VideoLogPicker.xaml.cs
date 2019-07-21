using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Presentation.Views.VideoLog;
using TGUApp.Models;
using TGUApp.Infrastructure;

namespace TGUApp.Presentation.Pages.VideoLog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VideoLogPicker : ContentPage
	{
		public VideoLogPicker ()
		{
			InitializeComponent ();

            PopulateVideos();
		}

        private void PopulateVideos()
        {
            Videos.Children.Clear();

            List<LogVideo> videos = App.Database.GetLogVideos();

            foreach (LogVideo video in videos)
            {
                VideoEntry entry = new VideoEntry(video.Id, video.FileName, video.Date, video.Time, video.UploadStatus);
                entry.VideoClicked += Entry_VideoClicked;
                entry.DeleteClicked += Entry_DeleteClicked;
                entry.UploadClicked += Entry_UploadClicked;
                Videos.Children.Add(entry);
            }
        }

        private void Entry_UploadClicked(object sender, EventArgs.LogVideoUploadClickedEventArgs e)
        {
            Internet.UploadVideo(e.Id, e.FileName);
        }

        private void Entry_DeleteClicked(object sender, EventArgs.LogVideoDeleteClickedEventArgs e)
        {
            FileSystem.DeleteLogVideo(e.FileName);

            PopulateVideos();
        }

        private void Entry_VideoClicked(object sender, EventArgs.LogVideoViewClickedEventArgs e)
        {
            Navigation.PushAsync(new VideoLogViewer(e.FileName));
        }
    }
}
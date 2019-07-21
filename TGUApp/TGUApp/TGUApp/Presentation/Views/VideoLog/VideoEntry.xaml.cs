using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TGUApp.Presentation.EventArgs;
using TGUApp.Enumerations;

namespace TGUApp.Presentation.Views.VideoLog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VideoEntry : ContentView
	{
        public event EventHandler<LogVideoViewClickedEventArgs> VideoClicked;
        public event EventHandler<LogVideoUploadClickedEventArgs> UploadClicked;
        public event EventHandler<LogVideoDeleteClickedEventArgs> DeleteClicked;

        public int VideoId { get; set; }
        public string VideoFileName { get; set; }

        public VideoEntry()
        {
            InitializeComponent();
        }

        public VideoEntry(int id, string fileName, string dateOf, string timeOf, UploadStatus status)
        {
            InitializeComponent();

            VideoId = id;
            FileName.Text = fileName;
            this.DateTime.Text = dateOf + "\r\n" + timeOf;

            VideoFileName = fileName;

            if (status == UploadStatus.Uploaded)
            {
                UploadVideo.IsVisible = false;
                VideoUploaded.IsVisible = true;
            }
            else if (status == UploadStatus.Uploading)
            {
                UploadVideo.IsVisible = false;
                VideoUploading.IsVisible = true;
            }

            if (App.VideoEntries.ContainsKey(id))
            {
                App.VideoEntries.Remove(id);
                App.VideoEntries.Add(id, this);
            }
        }

        private void ViewVideo_Clicked(object sender, System.EventArgs e)
        {
            VideoClicked(this, new LogVideoViewClickedEventArgs(VideoFileName));
        }

        private void DeleteVideo_Clicked(object sender, System.EventArgs e)
        {
            DeleteClicked(this, new LogVideoDeleteClickedEventArgs(VideoFileName));

            App.Database.DeleteVideo(VideoId);
        }

        private void UploadVideo_Clicked(object sender, System.EventArgs e)
        {
            UploadClicked(this, new LogVideoUploadClickedEventArgs(VideoId, VideoFileName));

            App.Database.SetUploadStatus(VideoId, UploadStatus.Uploading);

            App.VideoEntries.Add(VideoId, this);

            UploadVideo.IsVisible = false;
            VideoUploading.IsVisible = true;
        }

        public void SetUploaded()
        {
            VideoUploaded.IsVisible = true;
            VideoUploading.IsVisible = false;

            App.Database.SetUploadStatus(VideoId, UploadStatus.Uploaded);

            App.VideoEntries.Remove(VideoId);
        }
    }
}
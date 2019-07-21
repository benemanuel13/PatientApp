using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ARelativeLayout = Android.Widget.RelativeLayout;

using TGUApp.Presentation.Renderers;
using TGUApp.Droid;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(VideoPlayerRenderer))]
namespace TGUApp.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, ARelativeLayout>
    {
        VideoView view;
        ARelativeLayout layout;

        public VideoPlayerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    layout = new ARelativeLayout(Context);

                    view = new VideoView(Context);
                    layout.AddView(view);

                    ARelativeLayout.LayoutParams param = new ARelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
                    param.AddRule(LayoutRules.CenterInParent);

                    view.LayoutParameters = param;

                    //string file = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyVideos) + "/" + "TGUTest.mp4";
                    string file = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/" + e.NewElement.FileName;
                    view.SetVideoPath(file);
                    
                    //view.SetVideoURI(Android.Net.Uri.Parse("android.resource://com.companyname/" + Resource.Raw.TGUTest));

                    view.Start();

                    e.NewElement.Surface = view.Holder.Surface;

                    SetNativeControl(layout);
                }
            }
        }

        public Surface Surface
        {
            get
            {
                return view.Holder.Surface;
            }
        }
    }
}
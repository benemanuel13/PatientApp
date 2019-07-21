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
using Android.Graphics;

using TGUApp.Interfaces;
using TGUApp.Presentation.Renderers;
using TGUApp.Droid;

[assembly: ExportRenderer(typeof(CameraViewer), typeof(TextureViewRenderer))]

namespace TGUApp.Droid
{
    public class TextureViewRenderer : ViewRenderer<CameraViewer, ARelativeLayout>, IRenderer
    {
        CameraTextureView view;
        ARelativeLayout layout;
        CameraViewer Viewer { get; set; }

        public TextureViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CameraViewer> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    layout = new ARelativeLayout(Context);

                    view = new CameraTextureView(Context);
                    layout.AddView(view);

                    ARelativeLayout.LayoutParams param = new ARelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
                    param.AddRule(LayoutRules.CenterInParent);

                    view.LayoutParameters = param;

                    Viewer = e.NewElement;
                    Viewer.Renderer = this;

                    SetNativeControl(layout);
                }
            }
        }

        private void RotateTheSucker(int width, int height, bool frontCamera)
        {
            Matrix matrix = new Matrix();

            RectF viewRect = new RectF(0, 0, width, height);
            RectF bufferRect = new RectF(0, 0, height, width);

            float centerX = viewRect.CenterX();
            float centerY = viewRect.CenterY();

            bufferRect.Offset(centerX - bufferRect.CenterX(), centerY - bufferRect.CenterY());

            matrix.SetRectToRect(viewRect, bufferRect, Matrix.ScaleToFit.Fill);

            if (frontCamera)
            {
                matrix.PreScale(1, -1, height, width);
                matrix.PreTranslate(0, centerY);
            }

            matrix.PostRotate(90 * (3 - 2), centerX, centerY);

            view.SetTransform(matrix);
        }

        public void SetSurface(bool frontCamera)
        {
            SurfaceTexture texture = view.SurfaceTexture;
            texture.SetDefaultBufferSize(1024, 768);
            Viewer.Surface = new Surface(texture);

            RotateTheSucker(view.Width, view.Height, frontCamera);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using TGUApp.Interfaces;

namespace TGUApp.Presentation.Renderers
{
    public class CameraViewer : View
    {
        public object Surface { get; set; }
        public IRenderer Renderer { get; set; }
    }
}

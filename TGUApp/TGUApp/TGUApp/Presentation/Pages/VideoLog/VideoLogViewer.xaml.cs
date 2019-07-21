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
	public partial class VideoLogViewer : ContentPage
	{
        public VideoLogViewer()
        {
            InitializeComponent();
        }

        public VideoLogViewer(string fileName)
        {
            InitializeComponent();

            VideoElement.FileName = fileName;
        }
    }
}
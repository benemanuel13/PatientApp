using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGUApp.Utility
{
    class FileImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            FileImageSourceConverter cnv = new FileImageSourceConverter();
            var imageSource = cnv.ConvertFromInvariantString(Source);

            return imageSource;
        }
    }
}

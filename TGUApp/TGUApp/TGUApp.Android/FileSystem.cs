using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using TGUApp.Interfaces;

using TGUApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(FileSystem))]
namespace TGUApp.Droid
{
    public class FileSystem : IFileSystem
    {
        public string GetBasePath()
        {
            return Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/";
        }

        public string GetViewModelsPath()
        {
            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/ViewModels"))
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/ViewModels");

            return Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/ViewModels/";
        }
    }
}
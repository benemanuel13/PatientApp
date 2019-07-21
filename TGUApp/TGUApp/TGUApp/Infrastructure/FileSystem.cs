using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Xamarin.Forms;

using TGUApp.Interfaces;

namespace TGUApp.Infrastructure
{
    public static class FileSystem
    {
        public static void DeleteLogVideo(string fileName)
        {
            IFileSystem system = DependencyService.Get<IFileSystem>();

            File.Delete(system.GetBasePath() + fileName);
        }
    }
}

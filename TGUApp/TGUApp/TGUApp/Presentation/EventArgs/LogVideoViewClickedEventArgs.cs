using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Presentation.EventArgs
{
    public class LogVideoViewClickedEventArgs : System.EventArgs
    {
        public string FileName { get; set; }

        public LogVideoViewClickedEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }
}

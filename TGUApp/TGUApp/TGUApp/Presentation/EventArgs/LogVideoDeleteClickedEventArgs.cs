using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Presentation.EventArgs
{
    public class LogVideoDeleteClickedEventArgs : System.EventArgs
    {
        public string FileName { get; set; }

        public LogVideoDeleteClickedEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }
}

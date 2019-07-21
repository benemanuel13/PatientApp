using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Presentation.EventArgs
{
    public class LogVideoUploadClickedEventArgs : System.EventArgs
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public LogVideoUploadClickedEventArgs(int id, string fileName)
        {
            Id = id;
            FileName = fileName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TGUApp.Interfaces
{
    public interface ICamera
    {
        string FileName { get; set; }
        object Surface { get; set; }

        void StartRecording(int camera, bool newWay);
        void StopRecording();
        int SwitchCamera(int oldCamera);

        bool IsFrontCamera(int camera);
    }
}

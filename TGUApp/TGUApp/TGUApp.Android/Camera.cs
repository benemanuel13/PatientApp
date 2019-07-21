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

using Android.Media;
using System.Threading;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using TGUApp.Interfaces;

using TGUApp.Droid;

#pragma warning disable 0618, 0612

[assembly: Xamarin.Forms.Dependency(typeof(Camera))]
namespace TGUApp.Droid
{
    public class Camera : ICamera
    {
        private MediaRecorder recorder;

        public object Surface { get; set; }
        public string FileName { get; set; }

        private int hdLvl;
        private Android.Hardware.Camera cam = null;

        private int currentCam;
        private bool currentWay = true;

        public static Activity CurrentActivity { get; set; }

        public bool IsFrontCamera(int camera)
        {
            CameraManager manager = (CameraManager)CurrentActivity.GetSystemService(Context.CameraService);

            string id = manager.GetCameraIdList()[camera];
            CameraCharacteristics chars = manager.GetCameraCharacteristics(id);
            LensFacing isFrontCamera = (LensFacing)(int)chars.Get(CameraCharacteristics.LensFacing);

            return isFrontCamera == LensFacing.Front;
        }

        public void StartRecording(int camera, bool newWay)
        {
            currentCam = camera;
            currentWay = newWay;

            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp"))
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp");

            Android.Hardware.Camera2.CameraManager manager = (Android.Hardware.Camera2.CameraManager)CurrentActivity.GetSystemService(Context.CameraService);

            string id = manager.GetCameraIdList()[camera];
            CameraCharacteristics chars = manager.GetCameraCharacteristics(id);
            hdLvl = (int)chars.Get(CameraCharacteristics.InfoSupportedHardwareLevel);

            if (!newWay)
                recorder.Release();

            recorder = new MediaRecorder();

            if (newWay)
            {
                manager.OpenCamera(id, new CameraStateCallback(), null);
                recorder.SetOrientationHint(90);
            }
            else// if (hdLvl == (int)InfoSupportedHardwareLevel.Limited || hdLvl == (int)InfoSupportedHardwareLevel.Legacy)
            {
                cam = Android.Hardware.Camera.Open(camera);
                recorder.SetCamera(cam);
                cam.Unlock();
                recorder.SetOrientationHint(270);
            }

            recorder.SetAudioSource(AudioSource.Camcorder);
            recorder.SetVideoSource(VideoSource.Camera);

            CamcorderProfile prof = CamcorderProfile.Get(camera, CamcorderQuality.High);
            recorder.SetProfile(prof);

            recorder.SetOutputFile(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/" + FileName);

            recorder.SetPreviewDisplay((Surface)Surface);
            
            recorder.Prepare();

            try
            {
                recorder.Start();
            }
            catch
            {
                StartRecording(camera, false);
            }
        }

        public int SwitchCamera(int oldCamera)
        {
            int camera = 0;

            if (!currentWay)
                camera = 1;

            Android.Hardware.Camera2.CameraManager manager = (Android.Hardware.Camera2.CameraManager)CurrentActivity.GetSystemService(Context.CameraService);

            string oldid = manager.GetCameraIdList()[oldCamera];
            CameraCharacteristics oldChars = manager.GetCameraCharacteristics(oldid);
            int oldHdLvl = (int)oldChars.Get(CameraCharacteristics.InfoSupportedHardwareLevel);

            recorder.Stop();

            if (oldCamera == 1)
            {
                cam.Lock();
                cam.Release();
            }
            //if (oldHdLvl == (int)InfoSupportedHardwareLevel.Full)
            //{
            //}
            //else if (oldHdLvl == (int)InfoSupportedHardwareLevel.Limited || oldHdLvl == (int)InfoSupportedHardwareLevel.Legacy)
                //recorder.SetCamera(null);
            
            string id = manager.GetCameraIdList()[camera];
            CameraCharacteristics chars = manager.GetCameraCharacteristics(id);
            hdLvl = (int)chars.Get(CameraCharacteristics.InfoSupportedHardwareLevel);

            if (camera == 0)
            {
                manager.OpenCamera(id, new CameraStateCallback(), null);
                recorder.SetOrientationHint(90);
            }
            else
            {
                cam = Android.Hardware.Camera.Open(camera);
                recorder.SetCamera(cam);
                cam.Unlock();
                recorder.SetOrientationHint(270);
            }

            recorder.SetAudioSource(AudioSource.Camcorder);
            recorder.SetVideoSource(VideoSource.Camera);

            CamcorderProfile prof = CamcorderProfile.Get(camera, CamcorderQuality.High);
            recorder.SetProfile(prof);

            recorder.SetPreviewDisplay((Surface)Surface);

            recorder.Prepare();
            recorder.Start();

            return camera;
        }

        public void StopRecording()
        {
            if (!currentWay)
            {
                cam.Lock();
                cam.Release();
            }

            recorder.Stop();
            recorder.Release();
        }
    }
}
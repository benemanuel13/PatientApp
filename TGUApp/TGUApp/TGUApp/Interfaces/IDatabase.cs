using System;
using System.Collections.Generic;
using System.Text;

using TGUApp.Models;
using TGUApp.Enumerations;

namespace TGUApp.Interfaces
{
    public interface IDatabase
    {
        List<LogVideo> GetLogVideos();
        int GetNewVideoId();
        void SetUploadStatus(int id, UploadStatus status);
        void AddVideo(int id, string filename);
        void DeleteVideo(int id);
    }
}

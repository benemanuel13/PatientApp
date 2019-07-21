using System;
using System.Collections.Generic;
using System.Text;

using TGUApp.Interfaces;
using TGUApp.Models;
using TGUApp.Enumerations;

namespace TGUApp.Infrastructure
{
    class Database : IDatabase
    {
        public List<LogVideo> GetLogVideos()
        {
            List<LogVideo> videos = new List<LogVideo>();

            //videos.Add(new LogVideo(1, "Test.mp4", false));

            return videos;
        }

        public int GetNewVideoId()
        {
            return 0;
        }

        public void SetUploadStatus(int id, UploadStatus status)
        {
        }

        public void AddVideo(int id, string filename)
        {
        }

        public void DeleteVideo(int id)
        {
        }
    }
}

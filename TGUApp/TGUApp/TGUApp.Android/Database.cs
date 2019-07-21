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

using System.Data;
using System.Xml;
using Mono.Data.Sqlite;

using TGUApp.Interfaces;
using TGUApp.Models;
using TGUApp.Enumerations;

using TGUApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Database))]
namespace TGUApp.Droid
{
    public class Database : IDatabase
    {
        public Database()
        {
            if (!Directory.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp"))
                Directory.CreateDirectory(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp");

            if (!File.Exists(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.dat"))
                InitialiseDatabase();
        }

        private void InitialiseDatabase()
        {
            //SqliteConnection.CreateFile(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.db3");
            //This is unnecessary but good for future work on DB.
            FileStream stream = File.Create(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.dat");
            stream.Close();
        }

        public List<LogVideo> GetLogVideos()
        {
            List<LogVideo> videos = new List<LogVideo>();

            string[] files = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp", "*.mp4");

            foreach (string file in files)
            {
                string shortName = file.Substring(file.LastIndexOf("/") + 1, file.Length - file.LastIndexOf("/") - 1);
                string dateOf = File.GetCreationTime(file).ToShortDateString();
                string timeOf = File.GetCreationTime(file).ToShortTimeString();

                int thisId = int.Parse(shortName.Substring(3, shortName.Length - 7));
                UploadStatus uploaded = GetUploadStatus(thisId);

                videos.Add(new LogVideo(thisId, shortName, dateOf, timeOf, uploaded));
            }

            videos.Sort();

            return videos;
        }

        public void AddVideo(int id, string filename)
        {
            StreamWriter writer = File.AppendText(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.dat");
            writer.WriteLine(id.ToString() + "," + filename + ",unsent");
            writer.Close();
        }

        private UploadStatus GetUploadStatus(int id)
        {
            FileStream stream = File.OpenRead(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.dat");
            StreamReader reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] data = line.Split(',');

                if (int.Parse(data[0]) == id)
                {
                    reader.Close();
                    stream.Close();

                    if (data[2] == "uploaded")
                    {
                        reader.Close();
                        stream.Close();

                        return UploadStatus.Uploaded;
                    }
                    else if (data[2] == "uploading")
                    {
                        reader.Close();
                        stream.Close();

                        return UploadStatus.Uploading;
                    }
                    else
                    {
                        reader.Close();
                        stream.Close();

                        return UploadStatus.Unsent;
                    }
                }
            }

            reader.Close();
            stream.Close();

            return UploadStatus.Unsent;
        }

        public void SetUploadStatus(int id, UploadStatus status)
        {
            FileStream stream = File.OpenRead(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.dat");
            StreamReader reader = new StreamReader(stream);

            FileStream newStream = File.OpenWrite(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.tmp");
            StreamWriter newWriter = new StreamWriter(newStream);

            string newStatus;

            if (status == UploadStatus.Unsent)
                newStatus = "unsent";
            else if (status == UploadStatus.Uploading)
                newStatus = "uploading";
            else
                newStatus = "uploaded";

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] data = line.Split(',');

                string newLine;

                if (int.Parse(data[0]) == id)
                    newLine = data[0] + "," + data[1] + "," + newStatus;
                else
                    newLine = line;

                newWriter.WriteLine(newLine);
            }

            reader.Close();
            stream.Close();

            newWriter.Close();
            newStream.Close();

            File.Delete(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.dat");
            File.Copy(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.tmp",
                Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.dat");
            File.Delete(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.tmp");
        }

        public void DeleteVideo(int id)
        {
            FileStream stream = File.OpenRead(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.dat");
            StreamReader reader = new StreamReader(stream);

            FileStream newStream = File.OpenWrite(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.tmp");
            StreamWriter newWriter = new StreamWriter(newStream);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] data = line.Split(',');

                if (!(int.Parse(data[0]) == id))
                    newWriter.WriteLine(line);
            }

            reader.Close();
            stream.Close();

            newWriter.Close();
            newStream.Close();

            File.Delete(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.dat");
            File.Copy(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.tmp",
                Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.dat");
            File.Delete(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp/TGUApp.tmp");
        }

        public int GetNewVideoId()
        {
            string[] files = Directory.GetFiles(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/TGUApp", "*.mp4");

            int maxId = 0;

            foreach (string file in files)
            {
                string shortName = file.Substring(file.LastIndexOf("/") + 1, file.Length - file.LastIndexOf("/") - 1);

                int thisId = int.Parse(shortName.Substring(3, shortName.Length - 7));

                if (thisId > maxId)
                    maxId = thisId;
            }

            return maxId + 1;
        }
    }
}
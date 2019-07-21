using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Xamarin.Forms;

using BensJson;

using TGUApp.Models;
using TGUApp.Interfaces;
using TGUApp.Presentation.Views.VideoLog;

#pragma warning disable 0168

namespace TGUApp.Infrastructure
{
    public static class Internet
    {
        public static bool InternetAvailable()
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://tguapp.benemanuel.net");
                req.Timeout = 5000;

                WebResponse res = req.GetResponse();

                res.Close();

                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }

        public static string GetViewModelJson(string viewName, string langCode)
        {
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

            //StringContent content = new StringContent(json, UTF32Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            try
            {
                response = (client.GetAsync("http://tguapp.benemanuel.net/api/Backend/GetViewModelJson?viewName=" + viewName + "&langCode=" + langCode)).Result;
            }
            catch
            {
                return null;
            }

            HttpStatusCode statusCode = response.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {
                string responseText = Task.Run(() => response.Content.ReadAsStringAsync()).Result;

                responseText = responseText.Replace("\\", ""); ;
                responseText = responseText.Substring(1, responseText.Length - 2);

                return responseText;
            }

            return null;
        }

        public static async Task<ServiceUser> GetServiceUser(int id)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //StringContent content = new StringContent(json, UTF32Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            try
            {
                response = (client.GetAsync("http://tguapp.benemanuel.net/api/Backend/GetServiceUser?id=" + id)).Result;
            }
            catch
            {
                return null;
            }

            HttpStatusCode statusCode = response.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {
                string responseText = await response.Content.ReadAsStringAsync();

                ServiceUser user = new JsonDeserializer<ServiceUser>().Deserialize(responseText);

                return user;
            }

            return null;
        }

        public static async Task<CarePlan> GetCarePlan(int id)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //StringContent content = new StringContent(json, UTF32Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            try
            {
                response = (client.GetAsync("http://tguapp.benemanuel.net/api/Backend/GetCarePlan?id=" + id)).Result;
            }
            catch
            {
                return null;
            }

            HttpStatusCode statusCode = response.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {
                string responseText = await response.Content.ReadAsStringAsync();

                CarePlan plan = new JsonDeserializer<CarePlan>().Deserialize(responseText);

                return plan;
            }

            return null;
        }

        public static async Task<CrisisPlan> GetCrisisPlan(int id)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));            

            HttpResponseMessage response = null;

            try
            {
                response = (client.GetAsync("http://tguapp.benemanuel.net/api/Backend/GetCrisisPlan?id=" + id)).Result;
            }
            catch
            {
                return null;
            }

            HttpStatusCode statusCode = response.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {
                string responseText = await response.Content.ReadAsStringAsync();

                CrisisPlan plan = new JsonDeserializer<CrisisPlan>().Deserialize(responseText);

                return plan;
            }

            return null;
        }

        public static async Task<RelapsePreventionPlan> GetRelapsePreventionPlan(int id)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = null;

            try
            {
                response = (client.GetAsync("http://tguapp.benemanuel.net/api/Backend/GetRelapsePreventionPlan?id=" + id)).Result;
            }
            catch
            {
                return null;
            }

            HttpStatusCode statusCode = response.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {
                string responseText = await response.Content.ReadAsStringAsync();

                RelapsePreventionPlan plan = new JsonDeserializer<RelapsePreventionPlan>().Deserialize(responseText);

                return plan;
            }

            return null;
        }

        public static async Task<Diary> GetDiary(int id)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = null;

            try
            {
                response = (client.GetAsync("http://tguapp.benemanuel.net/api/Backend/GetDiary?id=" + id)).Result;
            }
            catch
            {
                return null;
            }

            HttpStatusCode statusCode = response.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {
                string responseText = await response.Content.ReadAsStringAsync();

                Diary plan = new JsonDeserializer<Diary>().Deserialize(responseText);

                return plan;
            }

            return null;
        }

        public async static void UploadVideo(int id, string fileName)
        {
            HttpClient client = new HttpClient();

            MultipartFormDataContent form = new MultipartFormDataContent();
            //HttpContent content = new StringContent("fileToUpload");
            
            //form.Add(content, "fileToUpload");

            IFileSystem system = DependencyService.Get<IFileSystem>();
            string path = system.GetBasePath();

            var stream = new System.IO.FileStream(path + fileName, System.IO.FileMode.Open);
            HttpContent content = new StreamContent(stream);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "fileToUpload",
                FileName = fileName
            };

            form.Add(content);

            HttpResponseMessage response = null;

            try
            {
                response = await client.PostAsync("http://tguapp.benemanuel.net/api/Backend/PostPatientVideo", form);
            }
            catch (Exception ex)
            {
                return;
            }

            HttpStatusCode statusCode = response.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {
                VideoEntry entry = App.VideoEntries[id];

                entry.SetUploaded();
            }
        }

        public static void UploadVideoOld(int id, string fileName)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = null;

            Video newVideo = new Video();
            newVideo.FileName = fileName;

            IFileSystem system = DependencyService.Get<IFileSystem>();
            string path = system.GetBasePath();

            byte[] fileBytes = System.IO.File.ReadAllBytes(path + fileName);
            string fileData = System.Web.HttpUtility.UrlEncode(fileBytes);

            newVideo.FileData = fileData;

            string contentString = new JsonSerializer().Serialize(newVideo);
            StringContent content = new StringContent(contentString);

            try
            {
                response = (client.PostAsync("http://tguapp.benemanuel.net/api/Backend/PostPatientVideo", content)).Result;
            }
            catch(Exception ex)
            {
                return;
            }

            HttpStatusCode statusCode = response.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {
             
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.IO;

using BensJson;

using TGUWeb.Models.WebApiModels;
using TGUWeb.Infrastructure;

using BensJsonTranslatorWin;

namespace TGUWeb.Controllers
{
    public class BackendController : ApiController
    {
        [Route("api/Backend/GetViewModelJson")]
        public string GetViewModelJson([FromUri] string viewName, [FromUri] string langCode)
        {
            string path = "C:\\inetpub\\TguApp\\ViewModelJson\\";
            string fileName = viewName + "_" + langCode + ".json";

            string json;

            if (File.Exists(path + fileName))
            {
                FileStream stream = File.OpenRead(path + fileName);
                StreamReader reader = new StreamReader(stream);

                json = reader.ReadToEnd();

                reader.Close();
                stream.Close();
            }
            else
            {
                string englishFileName = viewName + "_en.json";

                FileStream stream = File.OpenRead(path + englishFileName);
                StreamReader reader = new StreamReader(stream);

                string englishJson = reader.ReadToEnd();

                reader.Close();
                stream.Close();

                JsonTranslator tramslator = new JsonTranslator();
                json = tramslator.Translate(langCode, englishJson);

                stream = File.Create(path + fileName);
                StreamWriter writer = new StreamWriter(stream);

                writer.Write(json);

                writer.Close();
                stream.Close();
            }

            return json;
        }

        public ServiceUser GetServiceUser([FromUri] int id)
        {
            return Database.GetServiceUser(id);
        }

        [Route("api/Backend/GetCarePlan")]
        public CarePlan GetCarePlan([FromUri] int id)
        {
            return Database.GetCarePlan(id);
        }

        [Route("api/Backend/GetCrisisPlan")]
        public CrisisPlan GetCrisisPlan([FromUri] int id)
        {
            return Database.GetCrisisPlan(id);
        }

        [Route("api/Backend/GetRelapsePreventionPlan")]
        public RelapsePreventionPlan GetRelapsePreventionPlan([FromUri] int id)
        {
            return Database.GetRelapsePreventionPlan(id);
        }

        [Route("api/Backend/GetDiary")]
        public Diary GetDiary([FromUri] int id)
        {
            return Database.GetDiary(id);
        }

        [Route("api/Backend/PostPatientVideo")]
        public HttpResponseMessage PostPatientVideo()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            string folder = AppDomain.CurrentDomain.BaseDirectory + "PatientVideoFiles";

            MultipartFormDataStreamProvider p = new MultipartFormDataStreamProvider(folder);
            p = System.Threading.Tasks.Task.Run(() => Request.Content.ReadAsMultipartAsync(p)).Result;

            string localName = "";
            string realName = "";

            try
            {
                localName = p.FileData[0].LocalFileName;
                realName = p.Contents[0].Headers.ContentDisposition.FileName;

                if (realName == "")
                    realName = "\"";

                FileInfo info = new FileInfo(localName);

                string newFile = folder + "\\" + realName;

                if (File.Exists(newFile))
                    File.Delete(newFile);

                info.CopyTo(newFile);
                info.Delete();
            }
            catch (Exception)
            {
                string errorMessage = "";

                if (realName == "\"")
                {
                    File.Delete(localName);
                    errorMessage = "No file selected to upload";
                }
                else
                    errorMessage = localName + "\r\n" + realName;

                HttpResponseMessage httpErrorMessage = new HttpResponseMessage(HttpStatusCode.NoContent);
            
                return httpErrorMessage;
            }

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            return message;
        }

        public string PostPatientVideoOld([FromBody] Video video)
        {
            byte[] fileData = System.Web.HttpUtility.UrlDecodeToBytes(video.FileData);

            string folder = AppDomain.CurrentDomain.BaseDirectory + "PatientVideoFiles";

            try
            {
                FileStream stream = new FileStream("c:\\inetpub\\TGUApp\\PatientVideoFiles\\" + video.FileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(fileData);
                writer.Flush();

                writer.Close();
                stream.Close();
            }
            catch
            {
                return "Failed: " + video.FileName;
            }

            return "Succeeded";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Xamarin.Forms;

using TGUApp.Infrastructure;
using TGUApp.Interfaces;

using BensJson;

namespace TGUApp.Presentation.ViewModels
{
    public class VMGenerator<T> where T : class
    {
        public T CreateView(string langCode)
        {
            string viewName = typeof(T).Name;

            IFileSystem system = DependencyService.Get<IFileSystem>();
            string path = system.GetViewModelsPath();
            string file = path + viewName + "_" + langCode + ".json";

            string json;

            if (!File.Exists(file))
            {
                json = Internet.GetViewModelJson(viewName, langCode);

                FileStream stream = File.Create(file);
                StreamWriter writer = new StreamWriter(stream);

                writer.Write(json);

                writer.Close();
                stream.Close();
            }
            else
            {
                FileStream stream = File.OpenRead(file);
                StreamReader reader = new StreamReader(stream);

                json = reader.ReadToEnd();

                reader.Close();
                stream.Close();
            }

            T newView = new JsonDeserializer<T>().Deserialize(json);

            return newView;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Http;
using System.Net.Http.Headers;

using BensJson;

namespace BensTranslatorWin
{
    public class TranslationClient
    {
        public TextTranslation Translate(string langCode, string text)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            TextTranslation tTrans = new TextTranslation(langCode, text);
            string contentText = new JsonSerializer().Serialize(tTrans);

            StringContent content = new StringContent(contentText, Encoding.UTF8, "application/json");

            HttpResponseMessage response = (client.PostAsync("Url of translation api here...", content)).Result;

            string result = (response.Content.ReadAsStringAsync()).Result;

            TextTranslation returned = new JsonDeserializer<TextTranslation>().Deserialize(result);

            return returned;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BensTranslatorWin;

namespace BensJsonTranslatorWin
{
    public class JsonTranslator
    {
        public string Translate(string langCode, string json)
        {
            StringBuilder newJson = new StringBuilder();

            int wordCount = 0;

            List<string> Words = new List<string>();

            ProcessClass(langCode, 0, json, ref newJson, ref Words, wordCount);

            List<string> TranslatedWords = new List<string>();

            StringBuilder completeTranslation = new StringBuilder();

            foreach (string word in Words)
                completeTranslation.Append("," + word);

            string translationWords = completeTranslation.ToString();

            TranslationClient client = new TranslationClient();
            string translated = client.Translate(langCode, translationWords.Substring(1, translationWords.Length - 1)).Text;

            string[] wordsTranslated = translated.Split(',');

            for (int i = 0; i < Words.Count(); i++)
            {
                string thisWord = wordsTranslated[i];

                newJson = newJson.Replace("{" + i + "}", thisWord);
            }
            
            return newJson.ToString();
        }

        public int ProcessClass(string langCode, int startPosition, string json, ref StringBuilder newJson, ref List<string> words, int wordCount)
        {
            bool inKey = false;
            StringBuilder Key = new StringBuilder();

            bool onString = false;
            StringBuilder NewString = new StringBuilder();

            bool inValue = false;
            StringBuilder NewValue = new StringBuilder();

            for (int i = startPosition; i < json.Length; i++)
            {
                char thisChar = json.Substring(i, 1).ToCharArray()[0];

                if (thisChar == '{')
                {
                    newJson.Append('{');

                    i = ProcessClass(langCode, i + 1, json, ref newJson, ref words, wordCount);
                }
                else if (thisChar == '}')
                {
                    newJson.Append('}');

                    return i;
                }
                else if (thisChar == '\"')
                {
                    if (!inKey && !inValue)
                    {
                        Key.Append('\"');

                        inKey = true;
                    }
                    else if (inKey)
                    {
                        Key.Append('\"');
                        newJson.Append(Key.ToString());

                        Key.Clear();

                        inKey = false;
                    }
                    else if (!inKey && inValue)
                    {
                        if (onString)
                        {
                            words.Add(NewString.ToString());

                            string translated = "{" + wordCount++ + "}";

                            string translatedString = "\"" + translated + "\"";

                            newJson.Append(translatedString);

                            NewString.Clear();

                            //At end of string value
                            onString = false;
                            inValue = false;
                        }
                        else
                        {
                            //NewString.Append('\"');
                            onString = true;
                            //inValue = false;
                        }
                    }
                }
                else if (thisChar == ':')
                {
                    inValue = true;

                    newJson.Append(':');
                }
                else if (thisChar == ',')
                {
                    if (inValue)
                    {    
                        newJson.Append(NewValue);
                    }

                    newJson.Append(',');

                    inKey = false;
                    inValue = false;
                    onString = false;
                }
                else
                {
                    if (inKey)
                        Key.Append(thisChar);
                    else if (inValue && !onString)
                    {
                        NewValue.Append(thisChar);
                    }
                    else if (onString)
                    {
                        NewString.Append(thisChar);
                    }
                }
            }

            return newJson.Length;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace BensJson
{
    public class JsonDeserializer<T>
    {
        public T Deserialize(string json)
        {
            RealObject<T> rootObject = new RealObject<T>();
            json = FormatJsonString(json);

            return rootObject.Deserialize(json);
        }

        private string FormatJsonString(string json)
        {
            bool inString = false;
            StringBuilder sb = new StringBuilder();

            foreach (char chr in json.ToCharArray())
            {
                if ((chr == ' ' && !inString) || (chr == '\r' && !inString) || (chr == '\n' && !inString))
                { }
                else if (chr == '\"')
                {
                    inString = !inString;
                    sb.Append('\"');
                }
                else
                    sb.Append(chr);
            }

            return sb.ToString();
        }
    }
}

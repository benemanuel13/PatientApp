using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace BensJson
{
    internal class JsonObject
    {
        Dictionary<string, object> values = new Dictionary<string, object>();

        public JsonObject(object obj)
        {
            Type thisType = obj.GetType();
            PropertyInfo[] infos = thisType.GetTypeInfo().GetProperties();

            foreach (PropertyInfo info in infos)
            {
                PropertyInfo inInfo = obj.GetType().GetTypeInfo().GetDeclaredProperty(info.Name);

                var getProp = inInfo.GetValue(obj);

                if (!getProp.GetType().GetTypeInfo().IsValueType)
                {
                    string name = getProp.GetType().Name;

                    if (name != "String" && name != "List`1" && !name.EndsWith("[]"))
                        values.Add(info.Name, new JsonObject(getProp));
                    else if (name == "String")
                        values.Add(info.Name, getProp);
                    else if (name == "List`1")
                        values.Add(info.Name, new JsonList(getProp));
                    else if (name.EndsWith("[]"))
                        values.Add(info.Name, new JsonList(getProp));
                }
                else
                    values.Add(info.Name, getProp);
            }
        }

        public string ToJsonString()
        {
            StringBuilder b = new StringBuilder();
            b.Append("{");

            foreach (KeyValuePair<string, object> obj in values)
            {
                string name = obj.Value.GetType().Name;
                if (name == "String" || name == "DateTime" || obj.Value.GetType().GetTypeInfo().IsEnum)
                {
                    string val;
                    if (name == "DateTime")
                        val = ((DateTime)obj.Value).ToString("yyyy-MM-ddTHH:mm:ss");
                    else
                        val = obj.Value.ToString();

                    b.Append("\"" + obj.Key + "\":" + "\"" + val + "\",\r\n");
                }
                else if (obj.Value is JsonObject)
                    b.Append("\"" + obj.Key + "\":" + ((JsonObject)obj.Value).ToJsonString() + ",\r\n");
                else if (obj.Value is JsonList)
                    b.Append("\"" + obj.Key + "\":" + ((JsonList)obj.Value).ToJsonString() + ",\r\n");
                else if (name == "Boolean")
                {
                    string val;

                    if ((bool)obj.Value == true)
                        val = "true";
                    else
                        val = "false";

                    b.Append("\"" + obj.Key + "\":" + val + ",\r\n");
                }
                else
                    b.Append("\"" + obj.Key + "\":" + obj.Value.ToString() + ",\r\n");
            }

            string varString = b.ToString();
            varString = varString.Substring(0, varString.Length - 3);

            varString = varString + "}";

            return varString;
        }
    }
}

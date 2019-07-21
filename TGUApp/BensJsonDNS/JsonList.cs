using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace BensJson
{
    internal class JsonList
    {
        List<object> objects = new List<object>();

        public JsonList(object obj)
        {
            if (obj.GetType().Name.EndsWith("[]"))
            {
                var upperB = obj.GetType().GetTypeInfo().GetMethod("GetUpperBound");
                int count = (int)upperB.Invoke(obj, new object[] { 0 }) + 1;

                if (count == 0)
                    return;

                MethodInfo[] methods = obj.GetType().GetTypeInfo().GetMethods();

                MethodInfo mthdInfo = null;

                foreach (MethodInfo method in methods)
                {
                    ParameterInfo[] pInfos = method.GetParameters();

                    if(pInfos.Count() == 1 && pInfos[0].ParameterType == new Int32().GetType() && method.Name == "GetValue")
                    {
                        mthdInfo = method;
                        break;
                    }
                }

                for (int i = 0; i < count; i++)
                {
                    var obj2 = mthdInfo.Invoke(obj, new object[] { i });

                    if (obj2.GetType().Name == "List`1" || obj2.GetType().Name.EndsWith("[]"))
                        objects.Add(new JsonList(obj2));
                    else if (obj2.GetType().GetTypeInfo().IsValueType)
                    {
                        objects.Add(obj2);
                    }
                    else
                        objects.Add(new JsonObject(obj2));
                }
            }
            else
            {
                PropertyInfo inInfo = obj.GetType().GetTypeInfo().GetDeclaredProperty("Count");
                int count = (int)inInfo.GetValue(obj);

                if (count == 0)
                    return;

                PropertyInfo pInfo = obj.GetType().GetTypeInfo().GetDeclaredProperty("Item");
                var myObj = pInfo.GetGetMethod();

                for (int i = 0; i < count; i++)
                {
                    var obj2 = myObj.Invoke(obj, new object[] { i });

                    if (obj2.GetType().Name == "List`1" || obj2.GetType().Name.EndsWith("[]"))
                        objects.Add(new JsonList(obj2));
                    else if(obj2.GetType().GetTypeInfo().IsValueType)
                    {
                        objects.Add(obj2);
                    }
                    else
                        objects.Add(new JsonObject(obj2));
                }
            }
        }

        public string ToJsonString()
        {
            if (objects.Count == 0)
            {
                return "[]";
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            foreach (object obj in objects)
                if (obj is JsonList)
                    sb.Append(((JsonList)obj).ToJsonString() + ",\r\n");
                else if (obj.GetType().GetTypeInfo().IsValueType)
                {
                    sb.Append(obj.ToString() + ",\r\n");
                }
                else
                    sb.Append(((JsonObject)obj).ToJsonString() + ",\r\n");

            string varString = sb.ToString();
            varString = varString.Substring(0, varString.Length - 3);

            varString = varString + "]";

            return varString;
        }
    }
}

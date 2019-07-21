using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace BensJson
{
    public class JsonSerializer
    {
       public string Serialize(object obj)
        {
            if (obj.GetType().Name == "List`1" || obj.GetType().Name.EndsWith("[]"))
            {
                JsonList rootList = new JsonList(obj);
                return rootList.ToJsonString();
            }
            else
            {
                JsonObject rootObject = new JsonObject(obj);
                return rootObject.ToJsonString();
            }
        }
    }
}

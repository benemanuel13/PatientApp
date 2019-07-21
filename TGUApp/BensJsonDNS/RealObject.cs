using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Reflection;

namespace BensJson
{
    internal class RealObject<T>
    {
        public T Deserialize(string json)
        {
            Type type = typeof(T);

            T newObj = default(T);

            if (type.Name.EndsWith("[]"))
            {
                ConstructorInfo cInfo = type.GetTypeInfo().DeclaredConstructors.First();

                //byte[] array = new byte[]();

                newObj = (T)cInfo.Invoke(new object[] { 1170 });

                int pos = ProcessJson(json, 0, newObj, false, typeof(T).GetTypeInfo().IsValueType);
            }
            else
            {
                ConstructorInfo cInfo = type.GetTypeInfo().DeclaredConstructors.First(c => c.GetParameters().Count() == 0);

                newObj = (T)cInfo.Invoke(new object[] { });

                int pos = ProcessJson(json, 0, newObj, false, newObj.GetType().GetTypeInfo().IsValueType);
            }

            return newObj;
        }

        private int ProcessJson(string json, int startPosition, object currentObject, bool inList, bool valueType)
        {
            bool onKey = false;
            StringBuilder key = new StringBuilder();

            string oldKey = "";

            bool onValue = false;
            StringBuilder value = new StringBuilder();

            bool transition = false;
            bool inValue = false;
            bool inParen = false;

            int arrayIndex = 0;

            for (int i = startPosition; i < json.Length; i++)
            {
                char thisChar = json.Substring(i, 1).ToCharArray()[0];

                //{
                    if (thisChar == '[')
                    {
                        if (inList && currentObject.GetType().Name == "List`1")
                        {
                            var prop = currentObject.GetType().GetTypeInfo().GetDeclaredProperty("Item");
                            var addMthd = currentObject.GetType().GetTypeInfo().GetMethod("Add");

                            object thisItem = Activator.CreateInstance(prop.PropertyType);

                            i = ProcessJson(json, i + 1, thisItem, true, thisItem.GetType().GetTypeInfo().IsValueType);

                            addMthd.Invoke(currentObject, new object[] { thisItem });
                        }
                        else if (inList && currentObject.GetType().Name.EndsWith("[]"))
                        {
                            MethodInfo[] methods = currentObject.GetType().GetTypeInfo().GetMethods();

                            MethodInfo mthdInfo = null;

                            foreach (MethodInfo method in methods)
                            {
                                if (method.Name == "SetValue" && method.GetParameters().Count() == 2)
                                {
                                    mthdInfo = method;
                                    break;
                                }
                            }

                            object thisItem = Activator.CreateInstance(currentObject.GetType().GetElementType());

                            i = ProcessJson(json, i + 1, thisItem, true, thisItem.GetType().GetTypeInfo().IsValueType);

                            mthdInfo.Invoke(currentObject, new object[] { thisItem, arrayIndex });

                            arrayIndex++;
                        }

                        inList = true;
                    }
                    else if (thisChar == ']')
                    {
                        if (inList && key.ToString() == "")
                            return i;

                        inList = false;
                    }
                    else if (thisChar == '{')
                    {
                        if (onValue && !inList)
                        {
                            PropertyInfo info = currentObject.GetType().GetTypeInfo().GetDeclaredProperty(key.ToString());
                            object o = Activator.CreateInstance(info.PropertyType);

                            var setProp = info.GetSetMethod();
                            setProp.Invoke(currentObject, new object[] { o });

                            i = ProcessJson(json, i + 1, o, false, false);
                        }
                        else if (onValue && key.ToString() != "" && currentObject.GetType().GetTypeInfo().GetDeclaredProperty(key.ToString()).PropertyType.GetTypeInfo().IsClass && currentObject.GetType().GetTypeInfo().GetDeclaredProperty(key.ToString()).PropertyType.Name != "List`1")
                        {
                            PropertyInfo info = currentObject.GetType().GetTypeInfo().GetDeclaredProperty(key.ToString());
                            object o = Activator.CreateInstance(info.PropertyType);

                            var setProp = info.GetSetMethod();
                            setProp.Invoke(currentObject, new object[] { o });

                            i = ProcessJson(json, i + 1, o, false, false);
                        }
                        else if (inList)
                        {
                            if (key.ToString() == "")
                                key.Append(oldKey);

                            if (currentObject.GetType().Name == "List`1")
                            {
                                var prop = currentObject.GetType().GetTypeInfo().GetDeclaredProperty("Item");
                                var addMthd = currentObject.GetType().GetTypeInfo().GetMethod("Add");

                                object thisItem = Activator.CreateInstance(prop.PropertyType);

                                i = ProcessJson(json, i + 1, thisItem, true, false);

                                addMthd.Invoke(currentObject, new object[] { thisItem });
                            }
                            else if (currentObject.GetType().Name.EndsWith("[]"))
                            {
                                MethodInfo[] methods = currentObject.GetType().GetTypeInfo().GetMethods();

                                MethodInfo mthdInfo = null;

                                foreach (MethodInfo method in methods)
                                {
                                    if (method.Name == "SetValue" && method.GetParameters().Count() == 2)
                                    {
                                        mthdInfo = method;
                                        break;
                                    }
                                }

                                object thisItem = Activator.CreateInstance(currentObject.GetType().GetElementType());

                                i = ProcessJson(json, i + 1, thisItem, true, thisItem.GetType().GetTypeInfo().IsValueType);

                                mthdInfo.Invoke(currentObject, new object[] { thisItem, arrayIndex });

                                arrayIndex++;
                            }
                            else if (key.ToString() != "")
                            {
                                PropertyInfo info = currentObject.GetType().GetTypeInfo().GetDeclaredProperty(key.ToString());

                                var inf = info.GetValue(currentObject);

                                var prop = inf.GetType().GetTypeInfo().GetDeclaredProperty("Item");
                                var addMthd = inf.GetType().GetTypeInfo().GetMethod("Add");

                                object thisItem = Activator.CreateInstance(prop.PropertyType);

                                i = ProcessJson(json, i + 1, thisItem, true, false);

                                addMthd.Invoke(inf, new object[] { thisItem });
                            }
                        }
                        else
                        { }
                    }
                    else if (thisChar == '}')
                    {
                        if (key.ToString() != "")
                        {
                            PropertyInfo info = currentObject.GetType().GetTypeInfo().GetDeclaredProperty(key.ToString());

                            if (info.PropertyType == (new byte[0]).GetType())
                                ProcessProperty(currentObject, key.ToString(), value.ToString(), true);
                            else
                                ProcessProperty(currentObject, key.ToString(), value.ToString());
                            //ProcessProperty(currentObject, key.ToString(), value.ToString());
                        }

                        return i;
                    }
                    else
                    {
                        if (thisChar == '\"')
                        {
                            inParen = !inParen;

                            if (!onKey && !onValue)
                                onKey = true;
                            else if (onKey)
                                onKey = false;
                            else if (onValue && !transition)
                            {
                                onValue = false;
                                inValue = false;
                                transition = false;

                                PropertyInfo info = currentObject.GetType().GetTypeInfo().GetDeclaredProperty(key.ToString());

                                if(info.PropertyType == (new byte[0]).GetType())
                                    ProcessProperty(currentObject, key.ToString(), value.ToString(), true);
                                else
                                    ProcessProperty(currentObject, key.ToString(), value.ToString());

                                oldKey = key.ToString();

                                key = new StringBuilder();
                                value = new StringBuilder();
                            }
                        }
                        else if (thisChar == ',' && !inParen)
                        {
                            onValue = false;
                            inValue = false;
                            transition = false;

                            ProcessProperty(currentObject, key.ToString(), value.ToString());

                            oldKey = key.ToString();

                            key = new StringBuilder();
                            value = new StringBuilder();
                        }

                        if (thisChar == ':')
                        {
                            if (!onKey)
                            {
                                transition = true;
                                onValue = true;
                            }
                        }

                    if (onKey && thisChar != '\"')
                        key.Append(thisChar);
                    else if (onValue && thisChar != ':' && thisChar != '\"' && thisChar != ',')
                        value.Append(thisChar);
                    else if (onValue && thisChar == ':')
                        if (inValue)
                            value.Append(thisChar);
                        else
                            inValue = true;
                    else if (onValue && thisChar == ',' && inParen)
                        value.Append(thisChar);

                    }
                //}
            }

            return json.Length;
        }

        private void ProcessProperty(object currentObject, string key, object value, bool isByte = false)
        {
            if (currentObject.GetType().Name == "List`1" || (currentObject.GetType().Name.EndsWith("[]") && currentObject.GetType().GetElementType() != (new byte[0]).GetType()))
                return;
            else if (isByte)
            {
                byte[] bytes = Convert.FromBase64String(value.ToString());

                bytes = System.Net.WebUtility.UrlDecodeToBytes(bytes, 0, bytes.Length);

                PropertyInfo inf = currentObject.GetType().GetTypeInfo().GetDeclaredProperty(key.ToString());

                var setPrp = inf.GetSetMethod();
                setPrp.Invoke(currentObject, new object[] { bytes });

                return;
            }

            PropertyInfo info = currentObject.GetType().GetTypeInfo().GetDeclaredProperty(key.ToString());

            //if (info.PropertyType.IsClass && info.PropertyType != new String(' ', 0).GetType())
            if (info.PropertyType.GetTypeInfo().IsClass && info.PropertyType != typeof(string))
                return;

            object sender = null;

            if (info.PropertyType == typeof(string))
                sender = value.ToString();
            else
            {
                var v = Activator.CreateInstance(info.PropertyType);
                object[] methods;

                if (info.PropertyType.GetTypeInfo().IsEnum)
                    methods = v.GetType().GetTypeInfo().GetEnumUnderlyingType().GetTypeInfo().GetMethods();
                else
                    methods = v.GetType().GetTypeInfo().GetMethods();

                MethodInfo sProp = null;

                if (!info.PropertyType.GetTypeInfo().IsEnum)
                {
                    for (int i = 0; i < methods.Count(); i++)
                    {
                        sProp = v.GetType().GetTypeInfo().GetMethods()[i];

                        if (sProp.Name == "Parse" && sProp.GetParameters().Count() == 1 && sProp.GetParameters()[0].ParameterType == typeof(string))
                            break;
                    }
                }

                if (info.PropertyType.GetTypeInfo().IsEnum)
                    sender = Enum.Parse(v.GetType(), value.ToString());
                else
                    sender = sProp.Invoke(v, new object[] { value.ToString() });
            }
            
            var setProp = info.GetSetMethod();
            setProp.Invoke(currentObject, new object[] { sender });
        }
    }
}

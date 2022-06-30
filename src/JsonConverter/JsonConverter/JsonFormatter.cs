using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JsonConverter
{
    public class JsonFormatter
    {
        private static StringBuilder? jsonString = new StringBuilder();
        private static int number = 0;
        private static bool isLast = false;

        public static string Convert(object item)
        {
            number++;

            if (item is IList || item is Array)
            {
                if(item != null)
                {
                    var i = 0;
                    var j = 0;
                    foreach (var x in (IList)item)
                    {
                        i++;
                    }

                    jsonString = jsonString.Append("[\n");
                    foreach (var x in (IList)item)
                    {
                        j++;
                        if(i == j)
                        {
                            Convert(x);
                            for (var y = jsonString.Length-1; y >= 0; y--)
                            {
                                if (jsonString[y] == ',')
                                {
                                    jsonString = jsonString.Remove(y, 1);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Convert(x);
                        }
                    }
                    jsonString = jsonString.Append("]\n");
                }
            }
            else
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type type = assembly.GetType(item.ToString());

                PropertyInfo[] propertyInfo = type.GetProperties();
                ConstructorInfo constructor = type.GetConstructor(new Type[] { });
                object instance = constructor.Invoke(new object[] { });

                //foreach (var property in propertyInfo)
                //{
                //    if (property.PropertyType == typeof(object))
                //    {

                //    }

                //    var y = property.PropertyType;

                //    var classname = property.DeclaringType;

                //    var x = property.GetType();

                //    var a = property.GetValue(instance, null);

                //    var b = property.MemberType;
                //}

                foreach (var property in propertyInfo)
                {
                    var c = property.GetValue(item, null);
                    property.SetValue(instance, c);
                }


                jsonString = jsonString.Append("{\n");

                foreach (var property in propertyInfo)
                {
                    var name = property.PropertyType.Name;
                    if (name == "List`1" || name == "String[]" || name == "Int32[]" || name == "Single[]" || name == "Double[]")
                    {
                        var listValue = property.GetValue(item);

                        if(listValue != null)
                        {
                            jsonString = jsonString.Append($"\"{property.Name}\" : ");
                            jsonString = jsonString.Append("[\n");

                            foreach (var x in (IList)listValue)
                            {
                                if(x.GetType().Namespace == "System")
                                {
                                    if(x.GetType().Name == "Int32" || x.GetType().Name == "Double" || x.GetType().Name == "Single")
                                    {
                                        jsonString = jsonString.Append($"{x},\n");
                                    }
                                    else if (x.GetType().Name == "Boolean")
                                    {
                                        if (property.GetValue(instance).ToString() == "True")
                                        {
                                            jsonString = jsonString.Append($" true,\n");
                                        }
                                        else
                                        {
                                            jsonString = jsonString.Append($" false,\n");
                                        }
                                    }
                                    else
                                    {
                                        jsonString = jsonString.Append($"\"{x}\",\n");
                                    }
                                }
                                else
                                {
                                    Convert(x);
                                }
                            }
                            jsonString= jsonString.Remove(jsonString.Length - 2, 1);
                            if(propertyInfo.Last() == property)
                            {
                                jsonString = jsonString.Append("]\n");
                            }
                            else
                            {
                                jsonString = jsonString.Append("],\n");
                            }
                        }
                        else
                        {
                            jsonString = jsonString.Append($"\"{property.Name}\" : ");
                            if (propertyInfo.Last() == property)
                            {
                                jsonString = jsonString.Append("[]\n");
                            }
                            else
                            {
                                jsonString = jsonString.Append("[],\n");
                            }
                        }
                    }

                    else if (property.PropertyType.Namespace != "System")
                    {
                        if(propertyInfo.Last() == property)
                        {
                            isLast = true;
                        }
                        else
                        {
                            isLast = false;
                        }
                        jsonString = jsonString.Append($"\"{property.Name}\" : ");
                        Convert(property.GetValue(item));
                    }

                    else if (propertyInfo.Last() == property)
                    {
                        if (property.PropertyType.Name == "String")
                        {
                            if (property.GetValue(instance) == null)
                            {
                                jsonString = jsonString.Append($"\"{property.Name}\" : null\n");
                            }
                            else
                            {
                                jsonString = jsonString.Append($"\"{property.Name}\" : \"{property.GetValue(instance)}\"\n");
                            }
                        }
                        else if (property.PropertyType.Name == "Int32" || property.PropertyType.Name == "Double" || property.PropertyType.Name == "Single")
                        {
                            jsonString = jsonString.Append($"\"{property.Name}\" : {property.GetValue(instance)}\n");
                        }
                        else if (property.PropertyType.Name == "Boolean")
                        {
                            var x = property.GetValue(instance);
                            if (property.GetValue(instance).ToString() == "True")
                            {
                                jsonString = jsonString.Append($"\"{property.Name}\" : true\n");
                            }
                            else
                            {
                                jsonString = jsonString.Append($"\"{property.Name}\" : false\n");
                            }
                        }
                        else
                        {
                            jsonString = jsonString.Append($"\"{property.Name}\" : \"{property.GetValue(instance)}\"\n");
                        }
                    }

                    else
                    {
                        if (property.PropertyType.Name == "String")
                        {
                            if (property.GetValue(instance) == null)
                            {
                                jsonString = jsonString.Append($"\"{property.Name}\" : null,\n");
                            }
                            else
                            {
                                jsonString = jsonString.Append($"\"{property.Name}\" : \"{property.GetValue(instance)}\",\n");
                            }
                        }
                        else if (property.PropertyType.Name == "Int32" || property.PropertyType.Name == "Double" || property.PropertyType.Name == "Single")
                        {
                            jsonString = jsonString.Append($"\"{property.Name}\" : {property.GetValue(instance)},\n");
                        }
                        else if (property.PropertyType.Name == "Boolean")
                        {
                            //var x = property.GetValue(instance);
                            if (property.GetValue(instance).ToString() == "True")
                            {
                                jsonString = jsonString.Append($"\"{property.Name}\" : true,\n");
                            }
                            else
                            {
                                jsonString = jsonString.Append($"\"{property.Name}\" : false,\n");
                            }
                        }
                        else
                        {
                            jsonString = jsonString.Append($"\"{property.Name}\" : \"{property.GetValue(instance)}\",\n");
                        }
                    }
                }

                if (isLast)
                {
                    jsonString = jsonString.Append("}\n");
                    isLast = false;
                }
                else
                {
                    jsonString = jsonString.Append("},\n");
                }
                
            }

            number--;
            if (number == 0)
            {
                if (jsonString[jsonString.Length - 3] == '}')
                {
                    jsonString = jsonString.Remove(jsonString.Length-2, 1);
                }

                return jsonString.ToString();
            }
            return "";
        }
    }
}

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


        public static StringBuilder Convert(object item)
        {
            if (item is Array)
            {

            }
            if (item is IList)
            {
                jsonString = jsonString.Append("[\n");
                foreach (var x in (IList)item)
                {
                    Convert(x);
                }
                jsonString = jsonString.Append("]\n");

            }
            else
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type type = assembly.GetType(item.ToString());

                var rr = type.IsCollectible;

                PropertyInfo[] propertyInfo = type.GetProperties();
                ConstructorInfo constructor = type.GetConstructor(new Type[] { });
                object instance = constructor.Invoke(new object[] { });

                //type.IsPrimitive


                foreach (var property in propertyInfo)
                {
                    //if(property.PropertyType.Name == "List`1")
                    //{
                    //    Convert(property.GetValue(item));
                    //}

                    if (property.PropertyType == typeof(object))
                    {

                    }

                    var y = property.PropertyType;

                    var classname = property.DeclaringType;

                    var x = property.GetType();

                    var a = property.GetValue(instance, null);

                    var b = property.MemberType;
                }

                foreach (var property in propertyInfo)
                {
                    var c = property.GetValue(item, null);
                    property.SetValue(instance, c);
                }





                jsonString = jsonString.Append("{\n");

                foreach (var property in propertyInfo)
                {
                    if (property.PropertyType.Name == "List`1")
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
                                    if(x.GetType().Name == "Int32" || x.GetType().Name == "Double" || x.GetType().Name == "Boolean")
                                    {
                                        jsonString = jsonString.Append($"{x},\n");
                                    }
                                    else
                                    {
                                        jsonString = jsonString.Append($"\"{x}\",");
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
                        jsonString = jsonString.Append($"\"{property.Name}\" : ");
                        Convert(property.GetValue(item));
                    }

                    else if (propertyInfo.Last() == property)
                    {
                        jsonString = jsonString.Append($"\"{property.Name}\" : \"{property.GetValue(instance)}\"\n");
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
                        else if (property.PropertyType.Name == "Int32" || property.PropertyType.Name == "Double" || property.PropertyType.Name == "Boolean")
                        {
                            jsonString = jsonString.Append($"\"{property.Name}\" : {property.GetValue(instance)},\n");
                        }
                        else
                        {
                            jsonString = jsonString.Append($"\"{property.Name}\" : \"{property.GetValue(instance)}\",\n");
                        }
                    }
                }
                jsonString = jsonString.Append("},\n");
            }



            //Console.WriteLine(jsonString);


            return jsonString;
        }
    }
}

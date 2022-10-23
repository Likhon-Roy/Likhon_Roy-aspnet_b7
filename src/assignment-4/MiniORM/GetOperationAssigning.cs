using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniORM
{
    public class GetOperationAssigning
    {
        public static Object? _sstaticObject;
        public static string? dictionaryFullClassName;
        public static Object? dictionaryFullClassValue;


        public GetOperationAssigning(object staticObject)
        {
            _sstaticObject = staticObject;
        }
        public async Task<Object> AssignValue(Object item) //where T : class, new()
        {
            if (item is IList || item is Array)
            {
                foreach (var listDictionaryItem in (IList)item)
                {
                    await AssignValue(listDictionaryItem);
                }
            }
            else
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type type = assembly.GetType(item.ToString());

                PropertyInfo[] propertyInfo = type.GetProperties();
                ConstructorInfo constructor = type.GetConstructor(new Type[] { });
                object instance = constructor.Invoke(new object[] { });

                foreach (var ppp in propertyInfo)
                {
                    var c = ppp.GetValue(item, null);
                    ppp.SetValue(instance, c);
                }

                foreach (var property in propertyInfo)
                {
                    var name = property.PropertyType.Name;

                    if (name == "List`1" || name == "String[]" || name == "Int32[]" || name == "Single[]" || name == "Double[]")
                    {
                        var cd = property.PropertyType.FullName;
                        var classFullName = cd.Split('[', ',')[2];


                        if (classFullName == dictionaryFullClassName)
                        {
                            property.SetValue(_sstaticObject, dictionaryFullClassValue);
                            return _sstaticObject;
                        }
                        // if (property.GetValue(item) == null)
                        // {
                        //     var obj = assembly.CreateInstance(classFullName);
                        //     var objPropertyInfos = assembly.GetType(classFullName).GetProperties();
                        //     var tttttt = obj.GetType();
                        //     var className = obj.GetType().Name;

                        //     // var instsPropertys = insts.GetType().GetProperties();
                        //     // var w = ListFromType(tttttt);

                        //     // foreach (var x in (IList)insts)
                        //     // {
                        //     //     w.Add(x);
                        //     // }

                        //     // property.SetValue(instance, w);

                        var listValue = property.GetValue(item);

                        if (listValue != null)
                        {
                            foreach (var x in (IList)listValue)
                            {
                                if (x != null)
                                {
                                    await AssignValue(x);
                                }
                            }
                        }
                        // }
                    }
                    else if (property.PropertyType.Namespace != "System")
                    {
                        // if (property.GetValue(item) == null)
                        // {
                        //     var obj = assembly.CreateInstance(property.PropertyType.FullName);
                        //     var objPropertyInfos = assembly.GetType(property.PropertyType.FullName).GetProperties();

                        //     var tttttt = obj.GetType();
                        //     var className = obj.GetType().Name;

                        // var insts = await GetByIdQueryForSingle(objPropertyInfos, tttttt, obj);

                        // var instsPropertys = insts.GetType().GetProperties();

                        // property.SetValue(instance, insts);

                        if (property.Name == dictionaryFullClassName)
                        {
                            property.SetValue(instance, dictionaryFullClassValue);
                            return _sstaticObject;
                        }

                        if (property.GetValue(item) != null)
                        {
                            await AssignValue(property.GetValue(item));
                        }
                        // }
                    }
                }
            }

            return _sstaticObject;
        }

        public async Task<Object> GetByIdFromDictionary(List<Dictionary<string, Object>> dObject)
        {
            // StringBuilder? sb = new StringBuilder();
            foreach (var x in dObject)
            {
                var keys = x.Keys;
                foreach (var key in keys)
                {
                    var obj = x[key];
                    var type = obj.GetType().Name;
                    // if (type == "List`1")
                    // {
                    //     dictionaryFullClassName = key;
                    //     dictionaryFullClassValue = x[key];
                    // }
                    // else
                    // {
                    dictionaryFullClassName = key;
                    dictionaryFullClassValue = x[key];
                    await AssignValue(_sstaticObject);
                    // }
                }
            }
            return _sstaticObject;
        }
    }
}
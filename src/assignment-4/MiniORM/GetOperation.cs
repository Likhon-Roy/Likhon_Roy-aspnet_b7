using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyMiniOrm
{
    public class GetOperation<T> where T : class, new()
    {
        public QueryType queryType;
        public string? parentTable;
        private Guid? parentId;
        private bool needToReturn = false;
        private IDataUtility? _dataUtility;
        public static Object? _staticObject;
        public static bool firstTime = true;
        public static string? className;
        private string classFullName;
        private object iiiiiiiiiiiiiiiiiiiii;
        public static List<Dictionary<string, Object>>? dictioNaryObject = new List<Dictionary<string, Object>>();


        public GetOperation(IDataUtility dataUtility, object staticObject)
        {
            _dataUtility = dataUtility;
            _staticObject = (T)staticObject;
        }
        public async Task<object> GetRecursiveMethod<T>(T item) //where T : class, new()
        {
            bool hasParentTable = false;

            string? localParentTable = parentTable;
            Guid? localParentId = parentId;

            if (item is IList || item is Array)
            {
                foreach (var listDictionaryItem in (IList)item)
                {
                    await GetRecursiveMethod(listDictionaryItem);
                }
            }
            else
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type type = assembly.GetType(item.ToString());

                PropertyInfo[] propertyInfo = type.GetProperties();
                ConstructorInfo constructor = type.GetConstructor(new Type[] { });
                Object instance = constructor.Invoke(new object[] { });

                // T instance = (T)ins;

                /////////////

                // foreach (var property in propertyInfo)
                // {
                //     if (property.PropertyType == typeof(object))
                //     {

                //     }

                //     var y = property.PropertyType;

                //     var classname = property.DeclaringType;

                //     var listDictionaryItem = property.GetType();

                //     var a = property.GetValue(instance, null);

                //     var b = property.MemberType;
                // }

                ////////////

                if (firstTime)
                {
                    var innnn = await GetByIdQueryForSingle(propertyInfo, type, item);
                    instance = innnn;
                    _staticObject = (T)instance;

                    firstTime = false;
                }

                foreach (var ppp in propertyInfo)
                {
                    var c = ppp.GetValue(item, null);
                    ppp.SetValue(instance, c);

                    // ppp.SetValue(_staticObject, c);
                }

                foreach (var ppp in propertyInfo)
                {
                    if (ppp.PropertyType.Namespace != "System")
                    {
                        hasParentTable = true;
                        break;
                    }
                }
                if (hasParentTable)
                {
                    foreach (var ppp in propertyInfo)
                    {
                        if (ppp.Name == "Id")
                        {
                            parentTable = type.Name;
                            parentId = (Guid)ppp.GetValue(instance);

                            break;
                        }
                    }
                }

                foreach (var property in propertyInfo)
                {
                    var name = property.PropertyType.Name;

                    if (name == "List`1" || name == "String[]" || name == "Int32[]" || name == "Single[]" || name == "Double[]")
                    {
                        var cd = property.PropertyType.FullName;

                        classFullName = cd.Split('[', ',')[2];

                        if (property.GetValue(item) == null)
                        {
                            var obj = assembly.CreateInstance(classFullName);
                            var objPropertyInfos = assembly.GetType(classFullName).GetProperties();
                            var tttttt = obj.GetType();
                            className = obj.GetType().Name;

                            var insts = await GetByIdQueryForList(objPropertyInfos, tttttt, obj);

                            var instsPropertys = insts.GetType().GetProperties();
                            var w = ListFromType(tttttt);

                            foreach (var x in (IList)insts)
                            {
                                w.Add(x);
                            }

                            property.SetValue(instance, w);

                            var d = new Dictionary<string, object>();
                            d.Add(className, w);
                            dictioNaryObject.Add(d);

                            await GetRecursiveMethod(w);



                            // var staticObjectProperties = _staticObject.GetType().GetProperties();
                            // foreach (var sss in staticObjectProperties)
                            // {
                            //     if (sss.PropertyType.Name == "List`1")
                            //     {
                            //         var fuN = sss.PropertyType.FullName.Split('[', ',')[2];
                            //         if (classFullName == fuN)
                            //         {
                            //             sss.SetValue(_staticObject, w);
                            //         }
                            //     }
                            //     else if (property.PropertyType.Namespace != "System")
                            //     {
                            //         sss.SetValue(_staticObject, w);
                            //     }
                            // }

                        }
                    }
                    else if (property.PropertyType.Namespace != "System")
                    {
                        if (property.GetValue(item) == null)
                        {
                            var obj = assembly.CreateInstance(property.PropertyType.FullName);
                            var objPropertyInfos = assembly.GetType(property.PropertyType.FullName).GetProperties();

                            var tttttt = obj.GetType();
                            className = obj.GetType().Name;

                            var insts = await GetByIdQueryForSingle(objPropertyInfos, tttttt, obj);

                            var instsPropertys = insts.GetType().GetProperties();

                            property.SetValue(instance, insts);

                            var d = new Dictionary<string, object>();
                            d.Add(className, insts);
                            dictioNaryObject.Add(d);

                            await GetRecursiveMethod(insts);
                        }
                    }

                    iiiiiiiiiiiiiiiiiiiii = instance;
                }
            }

            parentId = localParentId;
            parentTable = localParentTable;

            return dictioNaryObject;
        }

        public IList ListFromType(Type type)
        {
            var listType = typeof(List<>);
            var constructedType = listType.MakeGenericType(type);

            return (IList)Activator.CreateInstance(constructedType);
        }

        private async Task<object> GetByIdQueryForSingle(PropertyInfo[] propertyInfo, Type type, object instance)
        {
            StringBuilder? sb = new StringBuilder();
            bool isPrimaryKeyAvaialable = false;
            sb.Append($"Select * from {type.Name}s where ");

            foreach (var ppp in propertyInfo)
            {
                if (ppp.Name == "Id")
                {
                    Guid? x = (Guid)ppp.GetValue(instance);

                    if (ppp.GetValue(instance) != null && Guid.Empty != x)
                    {
                        sb.Append($"{ppp.Name} = '{ppp.GetValue(instance)}'");
                        isPrimaryKeyAvaialable = true;
                        break;
                    }
                }
            }
            if (!isPrimaryKeyAvaialable)
            {
                if (parentTable != null && parentId != null && Guid.Empty != parentId)
                {
                    sb.Append($"{parentTable + "Id"} = '{parentId}'");
                    // parameters.Add(parentTable, parentId);
                }
            }

            var query = sb.ToString();

            var listDictionaryItem = await _dataUtility.GetDataAsync(query, null, System.Data.CommandType.Text);

            foreach (var x in listDictionaryItem)
            {
                var keys = x.Keys;

                foreach (string key in keys)
                {
                    foreach (var ppp in propertyInfo)
                    {
                        if (key == ppp.Name)
                        {
                            var value = x[key].GetType().FullName;

                            if (value != "System.DBNull")
                            {
                                ppp.SetValue(instance, x[key]);
                            }

                        }
                    }
                }
            }
            return instance;
        }

        private async Task<object> GetByIdQueryForList(PropertyInfo[] propertyInfo, Type type, object instance)
        {
            StringBuilder? sb = new StringBuilder();
            bool isPrimaryKeyAvaialable = false;
            sb.Append($"Select * from {type.Name}s where ");

            foreach (var ppp in propertyInfo)
            {
                if (ppp.Name == "Id")
                {
                    Guid? x = (Guid)ppp.GetValue(instance);

                    if (ppp.GetValue(instance) != null && Guid.Empty != x)
                    {
                        sb.Append($"{ppp.Name} = '{ppp.GetValue(instance)}'");
                        isPrimaryKeyAvaialable = true;
                        break;
                    }
                }
            }
            if (!isPrimaryKeyAvaialable)
            {
                if (parentTable != null && parentId != null && Guid.Empty != parentId)
                {
                    sb.Append($"{parentTable + "Id"} = '{parentId}'");
                    // parameters.Add(parentTable, parentId);
                }
            }

            var query = sb.ToString();

            var listDictionaryItem = await _dataUtility.GetDataAsync(query, null, System.Data.CommandType.Text);

            // count = listDictionaryItem.Count();
            var listOfObject = ListFromType(type);

            foreach (var x in listDictionaryItem)
            {
                var keys = x.Keys;
                Assembly assembly = Assembly.GetExecutingAssembly();
                var obj = assembly.CreateInstance(classFullName);
                var objPropertyInfos = assembly.GetType(classFullName).GetProperties();

                foreach (string key in keys)
                {
                    foreach (var ppp in objPropertyInfos)
                    {
                        if (key == ppp.Name)
                        {
                            var value = x[key].GetType().FullName;

                            if (value != "System.DBNull")
                            {
                                if (!(value == "System.String" && (ppp.Name == "Id" || ppp.Name == parentTable + "Id")))
                                {
                                    ppp.SetValue(obj, x[key]);
                                }
                            }
                        }
                    }
                }
                listOfObject.Add(obj);
            }
            return listOfObject;
        }










        public async Task AssignValue(Object item) //where T : class, new()
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

                foreach (var property in propertyInfo)
                {
                    var name = property.PropertyType.Name;

                    if (name == "List`1" || name == "String[]" || name == "Int32[]" || name == "Single[]" || name == "Double[]")
                    {
                        var cd = property.PropertyType.FullName;
                        classFullName = cd.Split('[', ',')[2];
                        // if (cd.Split('[', ',')[2] == classFullName)
                        // {
                        if (property.GetValue(item) == null)
                        {
                            var obj = assembly.CreateInstance(classFullName);
                            var objPropertyInfos = assembly.GetType(classFullName).GetProperties();
                            var tttttt = obj.GetType();
                            className = obj.GetType().Name;

                            var insts = await GetByIdQueryForList(objPropertyInfos, tttttt, obj);

                            var instsPropertys = insts.GetType().GetProperties();
                            var w = ListFromType(tttttt);

                            foreach (var x in (IList)insts)
                            {
                                w.Add(x);
                            }

                            property.SetValue(instance, w);

                            await AssignValue(w);
                        }
                        // }
                    }
                    else if (property.PropertyType.Namespace != "System")
                    {
                        if (property.GetValue(item) == null)
                        {
                            var obj = assembly.CreateInstance(property.PropertyType.FullName);
                            var objPropertyInfos = assembly.GetType(property.PropertyType.FullName).GetProperties();

                            var tttttt = obj.GetType();
                            className = obj.GetType().Name;

                            var insts = await GetByIdQueryForSingle(objPropertyInfos, tttttt, obj);

                            var instsPropertys = insts.GetType().GetProperties();

                            property.SetValue(instance, insts);
                            await AssignValue(insts);
                        }
                    }
                }
            }
        }
    }
}
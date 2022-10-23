using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyMiniOrm
{
    public class ReflectionClass<T>
    {
        public QueryType queryType;

        public string? parentTable;
        private Guid? parentId;

        private Dictionary<string, Guid> parentKeyName = new Dictionary<string, Guid>();

        private IDataUtility? _dataUtility;

        public ReflectionClass(IDataUtility dataUtility)
        {
            _dataUtility = dataUtility;
        }

        public async Task RecursiveMethod(object item)
        {
            bool isStoreId = false;
            // bool handleIfElse = false;

            string? localParentTable = parentTable;
            Guid? localParentId = parentId;

            if (item is IList || item is Array)
            {
                if (item != null)
                {
                    foreach (var x in (IList)item)
                    {
                        await RecursiveMethod(x);
                    }
                }
            }
            else
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type type = assembly.GetType(item.ToString());

                PropertyInfo[] propertyInfo = type.GetProperties();
                ConstructorInfo constructor = type.GetConstructor(new Type[] { });
                object instance = constructor.Invoke(new object[] { });

                /////////////

                // foreach (var property in propertyInfo)
                // {
                //     if (property.PropertyType == typeof(object))
                //     {

                //     }

                //     var y = property.PropertyType;

                //     var classname = property.DeclaringType;

                //     var x = property.GetType();

                //     var a = property.GetValue(instance, null);

                //     var b = property.MemberType;
                // }

                ////////////

                foreach (var property in propertyInfo)
                {
                    var c = property.GetValue(item, null);
                    property.SetValue(instance, c);
                }

                if (QueryType.Insert == queryType)
                {
                    await InsertQuery(propertyInfo, type, instance);
                }
                else if (QueryType.Update == queryType)
                {
                    await UpdateQuery(propertyInfo, type, instance);
                }
                else if (QueryType.DeleteByItem == queryType)
                {
                    await DeleteByItemQuery(propertyInfo, type, instance);
                }

                foreach (var ppp in propertyInfo)
                {
                    if (ppp.PropertyType.Namespace != "System")
                    {
                        isStoreId = true;
                        break;
                    }
                }
                if (isStoreId)
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
                        var listValue = property.GetValue(item);

                        if (listValue != null)
                        {
                            foreach (var x in (IList)listValue)
                            {
                                await RecursiveMethod(x);
                            }
                        }
                    }
                    else if (property.PropertyType.Namespace != "System")
                    {
                        await RecursiveMethod(property.GetValue(item));
                    }
                }

                // if (QueryType.Insert == queryType)
                // {
                //     await InsertQuery(propertyInfo, type, instance);
                // }
                // else if (QueryType.Update == queryType)
                // {
                //     await UpdateQuery(propertyInfo, type, instance);
                // }
                // else if (QueryType.DeleteByItem == queryType)
                // {
                //     await DeleteByItemQuery(propertyInfo, type, instance);
                // }
                // else if (QueryType.DeleteById == queryType)
                // {
                //     await DeleteByItemQuery(propertyInfo, type, instance);
                // }
                // else if (QueryType.GetById == queryType)
                // {
                //     await GetByIdQuery(propertyInfo, type, instance);
                // }
                // else if (QueryType.GetAll == queryType)
                // {
                //     await GetAllQuery(propertyInfo, type, instance);
                // }
            }

            parentId = localParentId;
            parentTable = localParentTable;

        }

        private async Task InsertQuery(PropertyInfo[] propertyInfo, Type type, object instance)
        {
            string? lastProperty = null;
            StringBuilder? sb = new StringBuilder();
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            sb.Append($"insert into {type.Name}s (");

            foreach (var ppp in propertyInfo)
            {
                if (ppp.PropertyType.Namespace == "System" && ppp.GetValue(instance) != null)
                {
                    lastProperty = ppp.Name;
                }
            }

            if (parentTable != null && parentId != null && Guid.Empty != parentId)
            {
                sb.Append($"{parentTable + "Id"}, ");
            }

            foreach (var ppp in propertyInfo)
            {
                if (lastProperty != null && lastProperty == ppp.Name)
                {
                    if (ppp.GetValue(instance) != null && ppp.PropertyType.Namespace == "System")
                    {
                        sb.Append($"{ppp.Name}) ");
                    }
                }
                else
                {
                    if (ppp.GetValue(instance) != null && ppp.PropertyType.Namespace == "System")
                    {
                        sb.Append($"{ppp.Name}, ");
                    }
                }
            }

            sb.Append($"values (");

            if (parentTable != null && parentId != null && Guid.Empty != parentId)
            {
                sb.Append($"@{parentTable + "Id"}, ");
                parameters.Add(parentTable + "Id", parentId);
            }

            foreach (var ppp in propertyInfo)
            {
                var x = ppp.PropertyType.AssemblyQualifiedName;
                var isDateTime = x.Contains("System.DateTime,");
                //     DateTime dateTimeFormat = DateTime.Parse(System.Convert.ToDateTime(ppp.GetValue(instance)).ToString("yyyy-MM-dd HH:mm:ss"));

                if (lastProperty != null && lastProperty == ppp.Name)
                {
                    if (ppp.GetValue(instance) != null && ppp.PropertyType.Namespace == "System")
                    {
                        if (isDateTime)
                        {
                            var dateTimeFormat = System.Convert.ToDateTime(ppp.GetValue(instance)).ToString("yyyy-MM-dd HH:mm:ss");

                            // sb.Append($"'{dateTimeFormat}')");
                            sb.Append($"@{ppp.Name})");

                            parameters.Add(ppp.Name, dateTimeFormat);
                        }
                        else
                        {
                            // sb.Append($"'{ppp.GetValue(instance)}')");
                            sb.Append($"@{ppp.Name})");

                            parameters.Add(ppp.Name, ppp.GetValue(instance));
                        }

                    }
                }
                else
                {
                    if (ppp.GetValue(instance) != null && ppp.PropertyType.Namespace == "System")
                    {
                        if (isDateTime)
                        {
                            var dateTimeFormat = System.Convert.ToDateTime(ppp.GetValue(instance)).ToString("yyyy-MM-dd HH:mm:ss");

                            // sb.Append($"'{dateTimeFormat}', ");
                            sb.Append($"@{ppp.Name}, ");

                            parameters.Add(ppp.Name, ppp.GetValue(instance));
                        }
                        else
                        {
                            // sb.Append($"'{ppp.GetValue(instance)}', ");
                            sb.Append($"@{ppp.Name}, ");

                            parameters.Add(ppp.Name, ppp.GetValue(instance));
                        }
                    }
                }
            }

            //string sql = $"insert into Courses (Id, Title, Fees, ClassStartDate) values(@xId, @xTitle, @xFees, @xClassStartDate)";

            var query = sb.ToString();

            await _dataUtility.ExecuteCommandAsync(query, parameters, System.Data.CommandType.Text);
        }

        private async Task UpdateQuery(PropertyInfo[] propertyInfo, Type type, object instance)
        {
            bool isPrimaryKeyAvaialable = false;
            StringBuilder? sb = new StringBuilder();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            sb.Append($"update {type.Name}s set ");

            foreach (var ppp in propertyInfo)
            {
                var x = ppp.PropertyType.AssemblyQualifiedName;
                var isDateTime = x.Contains("System.DateTime,");

                if ((parentTable + "Id" != ppp.Name) && ppp.Name != "Id")
                {
                    if (ppp.GetValue(instance) != null && ppp.PropertyType.Namespace == "System")
                    {
                        if (isDateTime)
                        {
                            var dateTimeFormat = System.Convert.ToDateTime(ppp.GetValue(instance)).ToString("yyyy-MM-dd HH:mm:ss");

                            sb.Append($"{ppp.Name} = @{ppp.Name}, ");

                            parameters.Add(ppp.Name, ppp.GetValue(instance));
                        }
                        else
                        {
                            sb.Append($"{ppp.Name} = @{ppp.Name}, ");

                            parameters.Add(ppp.Name, ppp.GetValue(instance));
                        }
                    }
                }
            }

            sb.Remove(sb.Length - 2, 1);

            sb.Append($"WHERE ");

            foreach (var ppp in propertyInfo)
            {
                if (ppp.Name == "Id")
                {
                    Guid? x = (Guid)ppp.GetValue(instance);

                    if (ppp.GetValue(instance) != null && Guid.Empty != x)
                    {
                        sb.Append($"{ppp.Name} = @{ppp.Name}");
                        parameters.Add(ppp.Name, ppp.GetValue(instance));

                        isPrimaryKeyAvaialable = true;
                        break;
                    }
                }
            }

            if (!isPrimaryKeyAvaialable)
            {
                if (parentTable != null && parentId != null && Guid.Empty != parentId)
                {
                    sb.Append($"{parentTable + "Id"} = @{parentTable + "Id"}");
                    parameters.Add(parentTable, parentId);
                }
            }

            var query = sb.ToString();

            await _dataUtility.ExecuteCommandAsync(query, parameters, System.Data.CommandType.Text);
        }


        private async Task DeleteByItemQuery(PropertyInfo[] propertyInfo, Type type, object instance)
        {
            string? lastProperty = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            StringBuilder? sb = new StringBuilder();
            sb.Append($"delete from {type.Name}s where ");

            foreach (var ppp in propertyInfo)
            {
                if (ppp.PropertyType.Namespace == "System" && ppp.GetValue(instance) != null)
                {
                    lastProperty = ppp.Name;
                }
            }

            if (parentTable != null && parentId != null && Guid.Empty != parentId)
            {
                sb.Append($"{parentTable + "Id"} = @{parentTable + "Id"} and ");
                parameters.Add(parentTable + "Id", parentId);
            }

            foreach (var ppp in propertyInfo)
            {
                var x = ppp.PropertyType.AssemblyQualifiedName;
                var isDateTime = x.Contains("System.DateTime,");
                if (ppp.GetValue(instance) != null && ppp.PropertyType.Namespace == "System")
                {
                    if (lastProperty != null && lastProperty == ppp.Name)
                    {
                        if (isDateTime)
                        {
                            var dateTimeFormat = System.Convert.ToDateTime(ppp.GetValue(instance)).ToString("yyyy-MM-dd HH:mm:ss");

                            sb.Append($"{ppp.Name} = @{ppp.Name}");

                            parameters.Add(ppp.Name, dateTimeFormat);
                        }
                        else
                        {
                            sb.Append($"{ppp.Name} = @{ppp.Name}");

                            parameters.Add(ppp.Name, ppp.GetValue(instance));
                        }
                    }
                    else
                    {
                        if (isDateTime)
                        {
                            var dateTimeFormat = System.Convert.ToDateTime(ppp.GetValue(instance)).ToString("yyyy-MM-dd HH:mm:ss");

                            sb.Append($"{ppp.Name} = @{ppp.Name} and ");

                            parameters.Add(ppp.Name, dateTimeFormat);

                        }
                        else
                        {
                            sb.Append($"{ppp.Name} = @{ppp.Name} and ");

                            parameters.Add(ppp.Name, ppp.GetValue(instance));
                        }
                    }
                }
            }

            var query = sb.ToString();

            await _dataUtility.ExecuteCommandAsync(query, parameters, System.Data.CommandType.Text);
        }

        private async Task DeleteByIdQuery(PropertyInfo[] propertyInfo, Type type, object instance)
        {
            StringBuilder? sb = new StringBuilder();

            sb.Append($"delete from {type.Name}s where ");

            foreach (var ppp in propertyInfo)
            {
                if (propertyInfo.Last() == ppp)
                {
                    sb.Append($"{ppp.Name} = '{ppp.GetValue(instance)}'");
                }
                else
                {
                    sb.Append($"{ppp.Name} = '{ppp.GetValue(instance)}' and ");
                }
            }

            var query = sb.ToString();

            await _dataUtility.ExecuteCommandAsync(query, null, System.Data.CommandType.Text);

        }

        private async Task GetByIdQuery(PropertyInfo[] propertyInfo, Type type, object instance)
        {
            StringBuilder? sb = new StringBuilder();

            sb.Append($"Select * from {type.Name}s where ");

            foreach (var ppp in propertyInfo)
            {
                if (ppp.Name == "Id")
                {
                    sb.Append($"{ppp.Name} = '{ppp.GetValue(instance)}'");
                }
                // else
                // {
                //     sb.Append($"{ppp.Name} = '{ppp.GetValue(instance)}' and ");
                // }
            }

            var query = sb.ToString();

            await _dataUtility.GetDataAsync(query, null, System.Data.CommandType.Text);

        }

        private async Task GetAllQuery(PropertyInfo[] propertyInfo, Type type, object instance)
        {
            StringBuilder? sb = new StringBuilder();

            sb.Append($"Select * from {type.Name}s");

            // foreach (var ppp in propertyInfo)
            // {
            //     if (ppp.Name == "Id")
            //     {
            //         sb.Append($"{ppp.Name} = '{ppp.GetValue(instance)}'");
            //     }
            //     // else
            //     // {
            //     //     sb.Append($"{ppp.Name} = '{ppp.GetValue(instance)}' and ");
            //     // }
            // }

            var query = sb.ToString();

            await _dataUtility.GetDataAsync(query, null, System.Data.CommandType.Text);

        }
    }
}
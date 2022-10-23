using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyMiniOrm
{
    public class MyORM<G, T> : BaseMyORM<G, T> where T : class, new()
    {
        public MyORM()
        {
        }
        public async override Task Insert(T item)
        {
            OperationOnItem<T> inst = new OperationOnItem<T>(new DataUtility());
            inst.queryType = QueryType.Insert;
            await inst.RecursiveMethod(item);
        }

        public async override Task Update(T item)
        {
            OperationOnItem<T> inst = new OperationOnItem<T>(new DataUtility());
            inst.queryType = QueryType.Update;
            await inst.RecursiveMethod(item);
        }

        public async override Task Delete(T item)
        {
            OperationOnItem<T> inst = new OperationOnItem<T>(new DataUtility());
            inst.queryType = QueryType.DeleteByItem;
            await inst.RecursiveMethod(item);


            // DeleteOperation<T> inst = new DeleteOperation<T>(new DataUtility());
            // inst.queryType = QueryType.DeleteByItem;
            // await inst.DeleteMethod(item);
        }

        public async override Task Delete(G id)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var u = typeof(T);

            var obj = assembly.CreateInstance(u.FullName);
            var tProperties = obj.GetType().GetProperties();

            foreach (var p in tProperties)
            {
                if (p.Name == "Id")
                {
                    p.SetValue(obj, id);
                }
            }

            var inst = new DeleteByIdOperation<T>(new DataUtility(), obj);
            inst.queryType = QueryType.GetById;
            List<Dictionary<string, Object>> r = await inst.GetRecursiveMethod(obj);

            await inst.DeleteByIdQuery(r);

        }

        public async override Task GetAll()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            var u = typeof(T);
            var obj = assembly.CreateInstance(u.FullName);


            // ReflectionClass<T> inst = new ReflectionClass<T>(new DataUtility());
            // inst.queryType = QueryType.GetAll;
            // await inst.RecursiveMethod(obj);
        }

        public async override Task GetById(G id)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var u = typeof(T);

            var obj = assembly.CreateInstance(u.FullName);
            var tProperties = obj.GetType().GetProperties();

            foreach (var p in tProperties)
            {
                if (p.Name == "Id")
                {
                    p.SetValue(obj, id);
                }
            }

            var inst = new GetOperation<T>(new DataUtility(), obj);
            inst.queryType = QueryType.GetById;
            var r = await inst.GetRecursiveMethod(obj);

            Console.WriteLine(JsonSerializer.Serialize(r));
        }
    }
}
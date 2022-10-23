using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyMiniOrm
{
    public interface IDataUtility
    {
        Task<List<Dictionary<string, object>>> GetDataAsync(string command,
            Dictionary<string, object> parameters, CommandType commandType);
        
        Task ExecuteCommandAsync(string command,
            Dictionary<string, object> parameters, CommandType commandType);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.BusinessObjects
{
    public class Book
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? AutherName { get; set; }
        public string? Publisher { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMiniOrm
{
    public class Phone
    {
        public Guid Id { get; set; }
        public string? Number { get; set; }
        public string? Extension { get; set; }
        public string? CountryCode { get; set; }
    }
}

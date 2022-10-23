using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMiniOrm
{
    public class Course
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public Instructor? Teacher { get; set; }
        public List<Topic>? Topics { get; set; }
        public double? Fees { get; set; }
        public List<AdmissionTest>? Tests { get; set; }

    }
}

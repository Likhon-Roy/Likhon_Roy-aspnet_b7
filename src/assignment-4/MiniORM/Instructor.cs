using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMiniOrm
{
    public class Instructor
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Address? InstructorAddress { get; set; }
        public List<Phone>? PhoneNumbers { get; set; }
    }
}

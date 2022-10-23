using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMiniOrm
{
    public class AdmissionTest
    {
        public Guid Id { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public double? TestFees { get; set; }
    }
}

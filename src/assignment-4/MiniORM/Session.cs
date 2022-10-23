using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMiniOrm
{
    public class Session
    {
        public Guid Id { get; set; }
        public int? DurationInHour { get; set; }
        public string? LearningObjective { get; set; }
    }
}

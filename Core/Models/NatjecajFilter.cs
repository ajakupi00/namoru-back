using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class NatjecajFilter
    {
        public IEnumerable<Guid>? Categories { get; set; }
        public IEnumerable<Guid>? Counties { get; set; }
        public IEnumerable<Guid>? Cities { get; set; }
        public bool Emergency { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class MestarCategory : BaseEntity
    {
        public Guid MestarId { get; set; }
        public Mestar Mestar { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

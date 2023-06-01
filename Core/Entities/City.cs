using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public Guid CountyID { get; set; }
        public County County { get; set; }
        public int ZipCode { get; set; }
        public Guid CountryID { get; set; }
        public Country Country { get; set; }
    }
}

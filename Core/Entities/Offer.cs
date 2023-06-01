using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Offer : BaseEntity
    {
        public Guid NatjecajId { get; set; }
        public Guid MestarId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public OfferStatus Status { get; set; }
        
    }
    public enum OfferStatus {PENDING, DENIED, ACCEPTED}
}

using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.interfaces
{
    public interface IOfferRepository
    {
        Task<Offer> GetCurrentOffer(Guid natjecajId, Guid mestarId);

    }
}

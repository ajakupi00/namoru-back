using Core.Entities;
using Core.interfaces;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly ApplicationContext _context;
        public OfferRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Offer> GetCurrentOffer(Guid natjecajId, Guid mestarId)
        {
            Offer offer = await _context.Offers
                        .SingleOrDefaultAsync(x => x.MestarId == mestarId && x.NatjecajId == natjecajId);

            return offer;
        }

    }
}

using AutoMapper;
using Core.Entities;
using Core.interfaces;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrazimMestra.Dtos;

namespace TrazimMestra.Controllers
{
    public class OfferController : BaseApiController
    {
        private readonly IGenericRepository<Offer> _repository;
        private readonly IMapper _mapper;
        private readonly IOfferRepository _offerRepository;

        public OfferController(IGenericRepository<Offer> repo, IMapper mapper, IOfferRepository offerRepository)
        {
            _repository = repo;
            _mapper = mapper;
            _offerRepository = offerRepository;
        }

        [HttpPost]
        //ADD AUTHORIZATION
        public IActionResult Add([FromBody]OfferDto newOffer)
        {
            //REPLACE THIS ID FROM HTTP CONTEXT
            Offer offer = new Offer();
            _mapper.Map(newOffer, offer);
            offer.MestarId = Guid.Parse("a3f63892-1425-403c-9e73-1059e6113826");
            offer.Status = OfferStatus.PENDING;
            _repository.Add(offer);
            return Ok();
        }

        [HttpGet("checkifapplied")]
        //ADD AUTHORIZATION
        public async Task<ActionResult<bool>> CheckIfApplied(string natjecajId)
        {
            //REPLACE MESTARID FROM HTTP CONTEXT
            string mestarId = "a3f63892-1425-403c-9e73-1059e6113826";
            Offer offer = await _offerRepository.GetCurrentOffer(Guid.Parse(natjecajId), Guid.Parse(mestarId));
            if(offer == null) { return Ok(false); }
            return Ok(true);
        }

    }
}

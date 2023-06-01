using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using TrazimMestra.Dtos;

namespace TrazimMestra.Controllers
{
    public class NatjecajController : BaseApiController
    {
        private readonly IGenericRepository<Natjecaj> _repository;
        private readonly IMapper _mapper;
        private readonly INatjecajRepository _natjecajRepository;
        public NatjecajController(IGenericRepository<Natjecaj> repo, IMapper mapper, INatjecajRepository natjecajRepository)
        {
            _repository = repo;
            _mapper = mapper;
            _natjecajRepository = natjecajRepository;
        }

        [HttpPost]
        public IActionResult Add([FromBody]NewNatjecajDto newNatjecaj)
        {
            Natjecaj natjecaj = new Natjecaj();
            //WHEN IMPLEMENTING LOGIN TAKE USER ID FROM HTTP CONTEXT
            natjecaj.UserID = Guid.Parse("21ffe414-42a4-422e-9db4-cb903191494d");
            natjecaj.Id = Guid.NewGuid();
            natjecaj.Created = DateTime.UtcNow;

            _mapper.Map(newNatjecaj, natjecaj);

            _repository.Add(natjecaj);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var natjecaj = await _repository.GetByIdAsync(id);
            if (natjecaj == null)
            {
                return NotFound();
            }

            _repository.Delete(natjecaj);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Natjecaj natjecaj)
        {
            _repository.Update(natjecaj);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Natjecaj>> GetById(Guid id)
        {
            var natjecaj = await _repository.GetByIdAsync(id);

            if (natjecaj == null)
            {
                return NotFound();
            }

            return Ok(natjecaj);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Natjecaj>>> ListAll()
        {
            var natjecaji = await _repository.ListAllAsync();
            return Ok(natjecaji);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Pagination<NatjecajDto>>> FilterNatjecajs([FromQuery]NatjecajFilter filter)
        {
            var natjecaji = await _natjecajRepository.GetFilteredNatjecajs(filter);

            IList<NatjecajDto> mappedResults = new List<NatjecajDto>();

            _mapper.Map(natjecaji, mappedResults);

            var paginated = new Pagination<NatjecajDto>
            {
                Count = natjecaji.Count(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageIndex,
                Data = mappedResults.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToList()
            };

            return Ok(paginated);
        }

        [HttpGet("natjecajbyid")]
        public async Task<ActionResult<NatjecajDto>> GetById(string natjecajId)
        {
            var natjecaj = await _natjecajRepository.GetNatjecajById(Guid.Parse(natjecajId));
            NatjecajDto natjecajDto = new NatjecajDto();
            _mapper.Map(natjecaj, natjecajDto);
            return Ok(natjecajDto);
        }

    }
}

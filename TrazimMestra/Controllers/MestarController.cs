using AutoMapper;
using Core.Entities;
using Core.interfaces;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using TrazimMestra.Dtos;

namespace TrazimMestra.Controllers
{
    public class MestarController : BaseApiController
    {
        private readonly IGenericRepository<Mestar> _repository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly INatjecajRepository _natjecajRepository;
        private readonly IMestarRepository _mestarRepository;
        private readonly IMapper _mapper;

        public MestarController(IGenericRepository<Mestar> repository, INatjecajRepository natjecajRepository, IMestarRepository mestarRepository, IMapper mapper, IGenericRepository<Category> categoryRepository)
        {
            _repository = repository;
            _natjecajRepository = natjecajRepository;
            _mestarRepository = mestarRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public IActionResult Add(Mestar mestar)
        {
            _repository.Add(mestar);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var mestar = await _repository.GetByIdAsync(id);
            
            if (mestar == null)
                return NotFound();
            
            _repository.Delete(mestar);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Mestar mestar)
        {            
            _repository.Update(mestar);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mestar>> GetByIdAsync(Guid id)
        {
            var mestar = await _repository.GetByIdAsync(id);
            if (mestar == null)
            {
                return NotFound();
            }

            return Ok(mestar);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Mestar>>> ListAllAsync()
        {
            var mestri = await _repository.ListAllAsync();
            return Ok(mestri);
        }

        [HttpGet("resolved-natjecaji/{mestarID}")]
        public async Task<ActionResult<IReadOnlyList<Natjecaj>>> ListResolvedNatjecaja(Guid mestarID)
        {
            var mestarNatjecaji = await _natjecajRepository.GetListResolvedNatjecaja(mestarID);

            if (mestarNatjecaji == null)
            { 
                return NotFound(); 
            }

            return Ok(mestarNatjecaji);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Pagination<MestarDto>>> ListByFilters([FromQuery] MestarFilter searchOptions)
        {
            var mestri = await _mestarRepository.Search(searchOptions);

            var mestriDto = _mapper.Map<IReadOnlyList<Mestar>, IReadOnlyList<MestarDto>>(mestri.ToList());

            var paginatedData = new Pagination<MestarDto>
            {
                Count = mestriDto.Count,
                Data = mestriDto,
                PageSize = searchOptions.PageSize,
                PageIndex = searchOptions.CurrentPage
            };

            return Ok(paginatedData);
        }

        [HttpGet("categories")]
        public async Task<IEnumerable<Category>> Categories()
        {
            return await _categoryRepository.ListAllAsync();
        }
    }
}

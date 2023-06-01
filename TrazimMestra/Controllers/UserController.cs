using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace TrazimMestra.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IGenericRepository<User> _repository;
        private readonly INatjecajRepository _natjecajRepository;

        public UserController(IGenericRepository<User> repository, INatjecajRepository natjecajRepository)
        {
            _repository = repository;
            _natjecajRepository = natjecajRepository;
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            _repository.Add(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _repository.Delete(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(User user)
        {
            _repository.Update(user);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<User>>> ListAllAsync()
        {
            var users = await _repository.ListAllAsync();
            return Ok(users);
        }

        [HttpGet("resolved-natjecaji/{userID}")]
        public async Task<ActionResult<IReadOnlyList<Natjecaj>>> ListResolvedNatjecaja(Guid userID)
        {
            var userNatjecaji = await _natjecajRepository.GetListResolvedNatjecaja(userID);
            return Ok(userNatjecaji);
        }
    }
}

using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using TrazimMestra.Dtos;

namespace TrazimMestra.Controllers
{
    public class AccountController : BaseApiController
    {
        private ApplicationContext _repo;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(ApplicationContext context, ITokenService tokenService, IMapper mapper)
        {
            _repo = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetCurrentUser(Guid id)
        {
            var baseUser = await _repo.Users
                                .Include(u => u.City)
                                .ThenInclude(u => u.County)
                                .FirstOrDefaultAsync(u => u.Id == id);


            if (baseUser == null)
            {
                return NotFound();
            }

            return Ok(baseUser);
        }

        [HttpGet("checkemail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            return Ok(await _repo.Users.AnyAsync(u => u.Email == email));
        }

        [HttpGet("login")]
        public async Task<ActionResult<string>> Login(string email, string password)
        {
            var baseUser = await _repo.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (baseUser == null)
            {
                return BadRequest("User does not exist");
            }

            return Ok(_tokenService.CreateToken(baseUser));
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterDto registerUser)
        {
            var userExist = await _repo.Users.FirstOrDefaultAsync(u => u.Email == registerUser.Email);
            if (userExist != null)
                return BadRequest("User with that email already exist!");

            var user = new User();
            _mapper.Map(registerUser, user);

            await _repo.Users.AddAsync(user);
            await _repo.SaveChangesAsync();

            string token = _tokenService.CreateToken(user);
            return Ok(user);

        }

        [HttpPost("update")]
        public async Task<ActionResult<User>> Update(UpdateUserDto updateUserDto)
        {
            var user = await _repo.Users
                .Include(u => u.City)
                .ThenInclude(c => c.County)
                .FirstOrDefaultAsync(u => u.Id == updateUserDto.Id);
            
            if (user == null)
                return BadRequest("User doesn't exist");

            _mapper.Map(updateUserDto, user);

            _repo.Users.Update(user);
            await _repo.SaveChangesAsync();
            return Ok(user);

        }

    }
}

using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Presentation.Entities.Creation;
using Presentation.Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _repositoy;
        private readonly IMapper _mapper;
        private readonly UserService _service;

        public UsersController(
            UserRepository repositoy,
            IMapper mapper,
            UserService service)
        {
            _repositoy = repositoy;
            _mapper = mapper;
            _service = service;
        }


        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            var allUsers = await _repositoy.GetUsersWithStatusAsync();
            return Ok(allUsers);
        }

        // GET: api/users/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User?>> Get(Guid id)
        {
            return await _repositoy.GetUserWithStatusAsync(id);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserForCreation userForCreation)
        {

            var newUser = _service.CreateUserWithStatus(userForCreation);
            _repositoy.AddUser(newUser);
            await _repositoy.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        // PUT api/user/5
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UserForCreation userForCreation)
        {
            var user = await _repositoy.GetUserWithStatusAsync(id);

            if (user == null)
                return NotFound();

            user.Username = userForCreation.Username;
            user.Password = userForCreation.Password;
            user.Email = userForCreation.Email;
            _repositoy.UpdateUser(user);
            await _repositoy.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _repositoy.GetUserWithStatusAsync(id);

            if (user == null)
                return NotFound();

            _repositoy.DeleteUser(user);
            await _repositoy.SaveChangesAsync();
            return NoContent();
        }
    }
}

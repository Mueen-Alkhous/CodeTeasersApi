using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Entities;
using Web.Api.Services;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _repositoy;
        private readonly UsersService _service;

        public UsersController(UserRepository repositoy, UsersService service)
        {
            _repositoy = repositoy;
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
            return CreatedAtAction(nameof(Get), newUser.Id, newUser);
        }

        // PUT api/user/5
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UserForCreation userForCreation)
        {
            var user = await _repositoy.GetUserWithStatusAsync(id);
            user.Username = userForCreation.Username;
            user.Password = userForCreation.Password;
            user.Email = userForCreation.Email;
            _repositoy.UpdateUser(user);
            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _repositoy.GetUserWithStatusAsync(id);
            _repositoy.DeleteUser(user);
            await _repositoy.SaveChangesAsync();
            return NoContent();
        }
    }
}

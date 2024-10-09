using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.Entities.Creation;
using Presentation.Entities.View;
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
        public async Task<ActionResult<List<UserForView>>> Get()
        {
            var users = await _repositoy.GetUsersWithStatusAsync();
            var usersForView = _mapper.Map<List<UserForView>>(users);
            return Ok(usersForView);
        }

        // GET: api/users/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserForView?>> Get(Guid id)
        {
            var user =  await _repositoy.GetUserWithStatusAsync(id);
            if (user == null)
                return NotFound();
            var userForView = _mapper.Map<UserForView>(user);
            return Ok(userForView);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserForCreation userForCreation)
        {
            var usernameExists = await _repositoy.IsUsernameExistsAsync(userForCreation.Username);
            var emailExists = await _repositoy.IsEmailExistsAsync(userForCreation.Email);

            // if either username or email already taken return a conflict(409) with the proper message
            if (usernameExists || emailExists)
            {
                List<string> errorMessages = [];

                if (usernameExists) 
                    errorMessages.Add("Username already exists");
                if (emailExists) 
                    errorMessages.Add("Email already exists");

                return Conflict(new { errors = errorMessages });
            }

            var newUser = _service.CreateUserWithStatus(userForCreation);
            _repositoy.AddUser(newUser);
            await _repositoy.SaveChangesAsync();
            var newUserForView = _mapper.Map<UserForView>(newUser);
            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUserForView);
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

        // PATCH api/user/5
        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<User> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var user = await _repositoy.GetUserWithStatusAsync(id);

            if (user == null)
                return NotFound();

            patchDoc.ApplyTo(user, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

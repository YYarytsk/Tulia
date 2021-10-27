using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.ControllerModels;
using WebAPI.Models.DBModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITuliaRepo _repo;

        public UserController(ITuliaRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("all")]
        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _repo.GetUserById(id);
            return Ok(user);
        }

        [HttpPost("register")]
        public ActionResult<User> CreateUser(User user)
        {
            var result = _repo.CreateUser(user);
            if(result != null)
            {
                return Ok(result);
            } else
            {
                return StatusCode(403, "Username already in use.");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoggedInUser user)
        {

            var result = await _repo.LogIn(new LoggedInUser(user.username, user.password));
            if(result !=null)
            {
                return Ok(result);
            } else
            {
                return StatusCode(404, "Invalid username or password");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if(await _repo.GetUserById(id) is User oldUser)
            
            {
                oldUser.NumberGroups ++;
                User updatedUser = await _repo.UpdateUser(id, oldUser);
                return Ok(updatedUser);
            };
            return NotFound();
            
        }

        [HttpPut("leavegroup/{id}")]
        public async Task<ActionResult<User>> UpdateUserWhenLeaveGroup(int id, User user)
        {
            if (await _repo.GetUserById(id) is User oldUser)

            {
                oldUser.NumberGroups--;
                User updatedUser = await _repo.UpdateUserWhenLeaveGroup(id, oldUser);
                return Ok(updatedUser);
            };
            return NotFound();

        }

        [HttpGet("userwithgroup/{id}")]
        public async Task<ActionResult<UserWithGroup>> GetUserWithGroup(int id)
        {
            var userwithgroup = await _repo.GetUserWithGroup(id);
            return Ok(userwithgroup);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool result = await _repo.DeleteUserById(id);
            if (result == false)
                return NotFound();
            return Ok();
        }
    }
}

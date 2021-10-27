using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.DBModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ITuliaRepo _repo;

        public GroupController(ITuliaRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("all")]
        public List<Group> GetAllGroups()
        {
            return _repo.GetAllGroups();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroupById(int id)
        {
            return await _repo.GetGroupById(id);
        }

        [HttpGet("groupwithposts/{id}")]
        public async Task<ActionResult<GroupIncludingPosts>> GetGroupIncludingPosts(int id)
        {
            var groupwithpost = await _repo.GetGroupIncludingPosts(id);
            return Ok(groupwithpost);
        }

        [HttpPost("create")]
        public ActionResult<Group> CreateGroup(Group group)
        {
            var result = _repo.CreateGroup(group);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(404, "GroupTitle already in use.");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Group>> UpdateGroup(int id)
        {
            var updatedGroup = await _repo.UpdateGroup(id);
            return Ok(updatedGroup);

        }

        [HttpPut("leavegroup/{id}")]
        public async Task<ActionResult<Group>> LeaveGroup(int id)
        {
            var updatedGroup = await _repo.LeaveGroup(id);
            return Ok(updatedGroup);

        }

        [HttpDelete("delete/{groupId}")]
        public ActionResult<Group> DeleteGroup(int groupId)
        {
            var result = _repo.DeleteGroup(groupId);
            if(result != null)
            {
                return Ok(result);
            } else
            {
                return StatusCode(404, "That group could not be found");
            }
        }
    }
}

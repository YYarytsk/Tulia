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
    public class PostController : ControllerBase
    {
        private readonly ITuliaRepo _repo;

        public PostController(ITuliaRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("create")]
        public ActionResult<Post> CreatePost(Post post)
        {
            var result = _repo.CreatePost(post);

            if(result != null)
            {
                return result;
            } else
            {
                return StatusCode(403);
            }
        }

        [HttpGet("all")]
        public List<Post> GetAllPosts()
        {
            return _repo.GetAllPosts();
        }

        [HttpGet("{groupId}")]
        public List<Post> GetPostsFromGroup(int groupId)
        {
            return _repo.GetPostsFromGroup(groupId);
        }

        [HttpGet("postwithcomments/{postId}")]
        public async Task<ActionResult<PostIncludingComments>> GetPostsIncludingComments(int postId)
        {
            var post = await _repo.GetPostIncludingComments(postId);
            return Ok(post);
        }


        [HttpDelete("/delete/{postId}")]
        public ActionResult<Post> DeletePost(int postId)
        {
            var result = _repo.DeletePost(postId);
            if(result != null)
            {
                return Ok(result);
            } else
            {
                return StatusCode(404, "Post could not be found.");
            }
        }
    }
}

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
    public class CommentController : ControllerBase
    {
        private readonly ITuliaRepo _repo;

        public CommentController(ITuliaRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("create")]
        public Comment CreateComment(Comment comment)
        {
            return _repo.CreateComment(comment);
        }

        [HttpGet("user")]
        public List<Comment> GetCommentsFromUser(User user)
        {
            return _repo.ListCommentsFromUser(user);
        }

        [HttpGet("post/{postId}")]
        public List<Comment> DisplayCommentsOnPost(int postId)
        {
            return _repo.DisplayCommentsOnPost(postId);
        }

        [HttpDelete("delete/{commentId}")]
        public ActionResult<Comment> DeleteComment(int commentId)
        {
            var result = _repo.DeleteComment(commentId);
            if(result != null)
            {
                return Ok(result);
            }else
            {
                return StatusCode(404, "That comment could not be found.");
            }
        }
    }
}

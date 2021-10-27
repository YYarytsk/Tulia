using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Models.Entities;
using Xunit;

namespace APITesting
{
    public class UnitTest1
    {
        [Fact]
        public void GetUserById()
        {
            var options = new DbContextOptionsBuilder<tuliadbContext>()
               .UseInMemoryDatabase(databaseName: "TuliaDatabase")
               .Options;

            using (var context = new tuliadbContext(options))
            {
                context.Users.Add(new WebAPI.Models.Entities.User {
                    Id = 1,
                    Username = "Liam",
                    Password = "Password",
                    FirstName = "Liam",
                    LastName = "Sloan",
                    NumberGroups = 0,
                    Role = "user"
                });
                context.SaveChanges();

                TuliaRepo repo = new TuliaRepo(context);

                var user = new WebAPI.Models.DBModels.User(1, "Liam", "Sloan", "Liam");
                var result = repo.GetUserById(1);

                Assert.Equal(user.Username, result.Result.Username);
            }
        }

        [Fact]
        public void ReturnNullForInvalidUser()
        {
            var options = new DbContextOptionsBuilder<tuliadbContext>()
               .UseInMemoryDatabase(databaseName: "TuliaDatabase")
               .Options;

            using (var context = new tuliadbContext(options))
            {
                context.Users.Add(new WebAPI.Models.Entities.User
                {
                    Id = 2,
                    Username = "Liam",
                    Password = "Password",
                    FirstName = "Liam",
                    LastName = "Sloan",
                    NumberGroups = 0,
                    Role = "user"
                });
                context.SaveChanges();

                TuliaRepo repo = new TuliaRepo(context);

                var user = new WebAPI.Models.DBModels.User(2, "greg", "Sloan", "Liam");
                var result = repo.GetUserById(1);

                Assert.NotEqual(user, result.Result);
            }
        }

        [Fact]
        public void DeleteInvalidPost()
        {
            var options = new DbContextOptionsBuilder<tuliadbContext>()
               .UseInMemoryDatabase(databaseName: "TuliaDatabase")
               .Options;

            using (var context = new tuliadbContext(options))
            {
                context.Posts.Add(new WebAPI.Models.Entities.Post
                {
                    UserId = 1,
                    Title = "title",
                    Body = "body",
                    CreatedTime = DateTime.Now,
                    GroupId = 1
                });
                context.SaveChanges();

                TuliaRepo repo = new TuliaRepo(context);

                PostController postController = new PostController(repo);
                var result = postController.DeletePost(0);
                var expected = postController.DeletePost(1);

                Assert.NotEqual(expected, result);
            }
        }

        [Fact]
        public void DeleteInvalidComment()
        {
            var options = new DbContextOptionsBuilder<tuliadbContext>()
               .UseInMemoryDatabase(databaseName: "TuliaDatabase")
               .Options;

            using (var context = new tuliadbContext(options))
            {
                context.Comments.Add(new WebAPI.Models.Entities.Comment
                {
                    UserId = 1,
                    PostId = 1,
                    Content = "hello",
                    Time = DateTime.Now
                });
                context.SaveChanges();

                TuliaRepo repo = new TuliaRepo(context);

                CommentController commentController = new CommentController(repo);
                var result = commentController.DeleteComment(0);
                var expected = commentController.DeleteComment(1);
                
                Assert.NotEqual(expected, result);
            }
        }

        [Fact]
        public void CheckUserResult()
        {
            var options = new DbContextOptionsBuilder<tuliadbContext>()
               .UseInMemoryDatabase(databaseName: "TuliaDatabase")
               .Options;

            using (var context = new tuliadbContext(options))
            {
                TuliaRepo repo = new TuliaRepo(context);

                var user = new WebAPI.Models.DBModels.User(1, "Liam", "Sloan", "Liam");
                var result = repo.GetUserById(1);

                Assert.IsType<WebAPI.Models.DBModels.User>(result.Result);
            }
        }
    }
}

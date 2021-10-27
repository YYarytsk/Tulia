using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.ControllerModels;
using WebAPI.Models.Entities;

namespace WebAPI.Models
{
    public class TuliaRepo : ITuliaRepo
    {
        private readonly tuliadbContext _context;

        public TuliaRepo(tuliadbContext context)
        {
            _context = context;
        }

        public List<DBModels.User> GetAllUsers()
        {
            var users = _context.Users.ToList();
            List<DBModels.User> userList = new List<DBModels.User>();

            foreach(var user in users)
            {
                userList.Add(new DBModels.User(user.Id, user.FirstName, user.LastName, user.Username, user.Role, user.NumberGroups));
            }

            return userList;
        }

        public async Task<DBModels.User> GetUserById(int id)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (foundUser != null)
            {
                return new DBModels.User(foundUser.Id, foundUser.FirstName, foundUser.LastName, foundUser.Username, foundUser.Role, foundUser.NumberGroups);
            }
            return new DBModels.User();
        }

        public async Task<DBModels.UserWithGroup> GetUserWithGroup(int id)
        {
            var returnedUser = await _context.Users
                .AsQueryable()
                .Include(g => g.Groups)
                .ThenInclude(m => m.Memberships)
                .Select(u => new DBModels.UserWithGroup
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.Username,
                    Role = u.Role,
                    NumberGroups = u.NumberGroups,
                    Groups = u.Groups.Select(g => new DBModels.Group(g.Id, g.UserId, g.NumberMember, g.GroupTitle, g.Description)).ToList(),
                    Memberships = u.Memberships.Select(m => new DBModels.Membership(m.Id, m.UserId, m.GroupId)).ToList()
                }
            ).ToListAsync();
            DBModels.UserWithGroup singleUser = returnedUser.FirstOrDefault(p => p.Id == id);
            return singleUser;
        }
        public async Task<DBModels.MembershipWithGroup> GetMemberById(int id)
        {
            var foundMember = await _context.Memberships.FirstOrDefaultAsync(u => u.Id == id);
            if (foundMember != null)
            {
                return new DBModels.MembershipWithGroup(foundMember.Id, foundMember.GroupId, foundMember.UserId, foundMember.Group);
            }
            return new DBModels.MembershipWithGroup();
        }

        public async Task<DBModels.MembershipWithGroup> GetMemberByGroupId(int userid, int groupid)
        {
            var foundMember = await _context.Memberships.FirstOrDefaultAsync(u => u.UserId == userid && u.GroupId == groupid);
            if (foundMember != null)
            {
                //var foundGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == foundMember.GroupId);
                return new DBModels.MembershipWithGroup(foundMember.Id, foundMember.GroupId, foundMember.UserId, foundMember.Group);
            }
            return null;
        }
        public async Task<DBModels.MembershipWithGroup> GetMembershipWithGroup(int id)
        {
            var returnedMembership = await _context.Memberships
                .AsQueryable()
                .Include(g => g.Group).Select(m => new DBModels.MembershipWithGroup(m.Id, m.UserId, m.GroupId, m.Group))
                .ToListAsync();
                
                
            DBModels.MembershipWithGroup member = returnedMembership.FirstOrDefault(p => p.Id == id);
            if (member != null) return member;
            return new DBModels.MembershipWithGroup();
        }

        public DBModels.User CreateUser(DBModels.User user)
        {
            try
            {
                var duplicateUsername = _context.Users.Single(u => u.Username == user.Username);
                return null;
            } catch(System.InvalidOperationException e)
            {
                _context.Users.Add(new Entities.User
                {
                    Username = user.Username,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    NumberGroups = 0,
                    Role = "user"
                });
                _context.SaveChanges();

                return new DBModels.User(0, user.FirstName, user.LastName, user.Username);
            }
        }

        public async Task<DBModels.User> UpdateUser(int id, DBModels.User user)
        {
            Entities.User foundUser = await _context.Users.FindAsync(id);
            if (foundUser != null)
            {
                //everything keeps the same, except the numbergroup will +1 when they hit join
                foundUser.Id = foundUser.Id;
                foundUser.FirstName = foundUser.FirstName;
                foundUser.LastName = foundUser.LastName;
                foundUser.Username = foundUser.Username;
                foundUser.Password = foundUser.Password;
                foundUser.Role = foundUser.Role;
                foundUser.NumberGroups++;


                _context.Users.Update(foundUser);
                await _context.SaveChangesAsync();
                return new DBModels.User(foundUser.Id, foundUser.NumberGroups);
            }
            return new DBModels.User();
        }

        public async Task<DBModels.User> UpdateUserWhenLeaveGroup(int id, DBModels.User user)
        {
            Entities.User foundUser = await _context.Users.FindAsync(id);
            if (foundUser != null)
            {
                //everything keeps the same, except the numbergroup will +1 when they hit join
                foundUser.Id = foundUser.Id;
                foundUser.FirstName = foundUser.FirstName;
                foundUser.LastName = foundUser.LastName;
                foundUser.Username = foundUser.Username;
                foundUser.Password = foundUser.Password;
                foundUser.Role = foundUser.Role;
                foundUser.NumberGroups--;


                _context.Users.Update(foundUser);
                await _context.SaveChangesAsync();
                return new DBModels.User(foundUser.Id, foundUser.NumberGroups);
            }
            return new DBModels.User();
        }

        public async Task<bool> DeleteUserById(int id)
        {
            Entities.User userToDelete = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public DBModels.Group CreateGroup(DBModels.Group group)
        {
            // check to see if that group name is taken already.
            try
            {
                var duplicateGroupName = _context.Groups.Single(g => g.GroupTitle == group.GroupTitle);
                return null;
            } catch(System.InvalidOperationException)
            {
                _context.Groups.Add(new Entities.Group
                {
                    UserId = group.UserId,
                    NumberMember = 1,
                    GroupTitle = group.GroupTitle,
                    Description = group.Description
                });
                _context.SaveChanges();

                return group;
            }
        }

        public async Task<DBModels.Group> GetGroupById(int id)
        {
            var foundGroup = await _context.Groups.FirstOrDefaultAsync(u => u.Id == id);
            if (foundGroup != null)
            {
                return new DBModels.Group(foundGroup.Id, foundGroup.UserId, foundGroup.NumberMember, foundGroup.GroupTitle, foundGroup.Description);
            }
            return new DBModels.Group();
        }

        public async Task<DBModels.GroupIncludingPosts> GetGroupIncludingPosts(int id)
        {

            var returnedGroup = await _context.Groups
                .Include(p => p.Posts)
                .Select(g => new DBModels.GroupIncludingPosts
                {
                    Id = g.Id,
                    GroupTitle = g.GroupTitle,
                    Description = g.Description,
                    UserId = g.UserId,
                    NumberMember = g.NumberMember,
                    Posts = g.Posts.Select(p => new DBModels.Post(p.Id, p.UserId, p.Title, p.Body, p.CreatedTime)).ToList()
                }
                ).ToListAsync();
            DBModels.GroupIncludingPosts singleGroup = returnedGroup.FirstOrDefault(p => p.Id == id);
            return singleGroup;
            
        }

        public async Task<DBModels.PostIncludingComments> GetPostIncludingComments(int id)
        {

            var returnedPost = await _context.Posts
                .Include(c => c.Comments)
                .Select(p => new DBModels.PostIncludingComments
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    Title = p.Title,
                    Body = p.Body,
                    CreatedTime = p.CreatedTime,
                    Comments = p.Comments.Select(c => new DBModels.Comment(c.UserId, c.PostId, c.Content, c.Time)).ToList()
                }
                ).ToListAsync();
            DBModels.PostIncludingComments singlePost = returnedPost.FirstOrDefault(p => p.Id == id);
            return singlePost;

        }

        public List<DBModels.Group> GetAllGroups()
        {
            var groups = _context.Groups.ToList();
            List<DBModels.Group> listGroups = new List<DBModels.Group>();

            foreach(var group in groups)
            {
                listGroups.Add(new DBModels.Group(group.Id, group.UserId, group.NumberMember, group.GroupTitle, group.Description));
            }

            return listGroups;
        }

        public async Task<DBModels.Group> UpdateGroup(int id)
        {
            Entities.Group foundGroup = await _context.Groups.FindAsync(id);
            if (foundGroup != null)
            {
                foundGroup.Id = foundGroup.Id;
                foundGroup.UserId = foundGroup.UserId;
                foundGroup.GroupTitle = foundGroup.GroupTitle;
                foundGroup.NumberMember++;
                foundGroup.Description = foundGroup.Description;

                _context.Groups.Update(foundGroup);
                await _context.SaveChangesAsync();
                return new DBModels.Group(foundGroup.Id, foundGroup.NumberMember);
            }
            return new DBModels.Group();
        }

        public async Task<DBModels.Group> LeaveGroup(int id)
        {
            Entities.Group foundGroup = await _context.Groups.FindAsync(id);
            if (foundGroup != null)
            {
                foundGroup.Id = foundGroup.Id;
                foundGroup.UserId = foundGroup.UserId;
                foundGroup.GroupTitle = foundGroup.GroupTitle;
                foundGroup.NumberMember--;
                foundGroup.Description = foundGroup.Description;

                _context.Groups.Update(foundGroup);
                await _context.SaveChangesAsync();
                return new DBModels.Group(foundGroup.Id, foundGroup.NumberMember);
            }
            return new DBModels.Group();
        }

        public async Task<DBModels.Membership> CreateMembership(DBModels.Membership membership)
        {

            var newEntity = new Entities.Membership
            {
                UserId = membership.UserId,
                GroupId = membership.GroupId

            };
             
            var existingEntity = await GetMemberByGroupId(membership.UserId, membership.GroupId);
            if (existingEntity != null)
            {
                throw new Exception("You already joined this group!");
            }
            else
            {
                await _context.Memberships.AddAsync(newEntity);
                await _context.SaveChangesAsync();
                return membership;
            }
        }


        //public DBModels.User LogIn(LoggedInUser user)
        //{
        //    // check to see if username exists
        //    try
        //    {
        //        var loginUser = _context.Users.Single(u => u.Username == user.username);
        //        
        //        //if that succeeds, make sure password fits
        //        if(user.password == loginUser.Password)
        //        {
        //            return new DBModels.User(loginUser.Id, loginUser.Username, loginUser.Password, loginUser.FirstName, loginUser.LastName,
        //                loginUser.Role, loginUser.NumberGroups);
        //        } else
        //        {
        //            return new DBModels.User(0, "error", "error", "error");
        //        }
        //    } catch (System.InvalidOperationException)
        //    {
        //        // username could not be found
        //        // return an "error" user object
        //        return new DBModels.User(0, "error", "error", "error");
        //    }
        //}

        public async Task<DBModels.User> LogIn(LoggedInUser user)
        {
            Entities.User foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.username && u.Password == user.password);

            if (foundUser != null)
            {
                DBModels.User loginUser = await GetUserById(foundUser.Id);
                return loginUser;
            }else
            return null;
        }

        // adds a comment to the database
        public DBModels.Comment CreateComment(DBModels.Comment comment)
        {
            _context.Comments.Add(new Entities.Comment
            {
                UserId = comment.UserId,
                PostId = comment.PostId,
                Content = comment.Content,
                Time = DateTime.Now
            });
            _context.SaveChanges();
            return new DBModels.Comment(comment.UserId, comment.PostId, comment.Content, comment.Time);
        }

        // List all comments from a user
        public List<DBModels.Comment> ListCommentsFromUser(DBModels.User user)
        {
            // try to find the user
            try
            {
                var foundUser = _context.Users.Single(u => u.Username == user.Username);
                
                // if user is found, find user's comments
                try
                {
                    var comments = _context.Comments.Where(c => c.UserId == foundUser.Id).ToList();
                    List<DBModels.Comment> userComments = new List<DBModels.Comment>();

                    foreach(var comment in comments)
                    {
                        userComments.Add(new DBModels.Comment(comment.UserId, comment.PostId, comment.Content, comment.Time));
                    }

                    return userComments;
                } catch (System.InvalidOperationException)
                {
                    return null;
                }
            } catch(System.InvalidOperationException)
            {
                return null;
            }
        }

        // Create a new post
        public DBModels.Post CreatePost(DBModels.Post post)
        {
            try
            {
                _context.Posts.Add(new Entities.Post
                {
                    UserId = post.UserId,
                    Title = post.Title,
                    GroupId = post.GroupId,
                    Body = post.Body,
                    CreatedTime = DateTime.Now
                }) ;
                _context.SaveChanges();
                return new DBModels.Post(post.Id, post.UserId, post.Title, post.Body, post.CreatedTime, post.GroupId);
            } catch(System.InvalidOperationException)
            {
                return null;
            }
        }

        // see the last 15 posts
        public List<DBModels.Post> GetAllPosts()
        {
            var posts = _context.Posts.ToList();

            List<DBModels.Post> fetchedPosts = new List<DBModels.Post>();

            foreach(var post in posts)
            {
                fetchedPosts.Add(new DBModels.Post(post.Id, post.UserId, post.Title, post.Body, post.CreatedTime, post.GroupId));
            }

            return fetchedPosts;
        }

        // returns the last 10 posts from a specific group
        public List<DBModels.Post> GetPostsFromGroup(int groupId)
        {
            var posts = _context.Posts.Where(p => p.GroupId == groupId).ToList();

            List<DBModels.Post> fetchedPosts = new List<DBModels.Post>();

            foreach (var post in posts)
            {
                fetchedPosts.Add(new DBModels.Post(post.Id, post.UserId, post.Title, post.Body, post.CreatedTime, post.GroupId));
            }

            return fetchedPosts;
        }



        // remove a group
        public DBModels.Group DeleteGroup(int groupId)
        {
            try
            {
                var group = _context.Groups.Single(g => g.Id == groupId);
                _context.Groups.Remove(group);
                _context.SaveChanges();
                return new DBModels.Group(group.UserId, group.GroupTitle, group.Description);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }

        // displays comments from specific post from post id
        public List<DBModels.Comment> DisplayCommentsOnPost(int postId)
        {
            var comments = _context.Comments.Where(p => p.PostId == postId);
            List<DBModels.Comment> commentList = new List<DBModels.Comment>();

            foreach(var comment in comments)
            {
                commentList.Add(new DBModels.Comment(comment.UserId, comment.PostId, comment.Content, comment.Time));
            }

            return commentList;
        }

        public DBModels.Comment DeleteComment(int commentId)
        {
            try
            {
                var comment = _context.Comments.Single(c => c.Id == commentId);
                return new DBModels.Comment(comment.UserId, comment.PostId, comment.Content, comment.Time);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }

        public DBModels.Post DeletePost(int postId)
        {
            try
            {
                var post = _context.Posts.Single(p => p.Id == postId);
                _context.Posts.Remove(post);
                _context.SaveChanges();
                return new DBModels.Post(post.Id, post.UserId, post.Title, post.Body, post.CreatedTime, post.GroupId);
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }

        public async Task<bool> DeleteMembership(int id)
        {
            Entities.Membership membershiptoToDelete = await _context.Memberships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershiptoToDelete != null)
            {
                _context.Memberships.Remove(membershiptoToDelete);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        
    }
}

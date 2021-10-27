using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.ControllerModels;
using WebAPI.Models.DBModels;

namespace WebAPI.Models
{
    public interface ITuliaRepo
    {
        public List<User> GetAllUsers();

        public Task<User> GetUserById(int id);

        public User CreateUser(User user);

        public Task<User> UpdateUser(int id, User user);

        public Task<User> UpdateUserWhenLeaveGroup(int id, User user);

        public Task<UserWithGroup> GetUserWithGroup(int id);

        public Task<MembershipWithGroup> GetMemberById(int id);

        public Task<MembershipWithGroup> GetMemberByGroupId(int userid, int groupid);

        public Task<MembershipWithGroup> GetMembershipWithGroup(int id);
        
        public Group CreateGroup(Group group);

        public List<Group> GetAllGroups();
        public Task<GroupIncludingPosts> GetGroupIncludingPosts(int id);
        public Task<Group> GetGroupById(int id);

        public Task<Group> UpdateGroup(int id);

        public Task<Group> LeaveGroup(int id);

        public Task<Membership> CreateMembership(Membership membership);

        public Task<bool> DeleteMembership(int id);

        public Task<User> LogIn(LoggedInUser user);

        public Task<bool> DeleteUserById(int id);

        public Comment CreateComment(Comment comment);

        public List<Comment> ListCommentsFromUser(User user);

        public Post CreatePost(Post post);

        public List<Post> GetAllPosts();

        public Task<PostIncludingComments> GetPostIncludingComments(int id);

        public List<Post> GetPostsFromGroup(int groupId);

        public Group DeleteGroup(int groupId);

        public List<Comment> DisplayCommentsOnPost(int postId);

        public Comment DeleteComment(int commentId);

        public Post DeletePost(int postId);
    }
}

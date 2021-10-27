## User Controller
* Login -> `/api/user/login/(Username, Password)`
* Register -> `/api/user/register/(User object)`
* Display all users -> `/api/user`

## Group Controller 
* Display all groups -> `/api/group/all`
* Create group -> `/api/group/create/(Group object)`
* Delete group -> `/api/group/delete/(Group id)`

## Post Controller
* Create post -> `/api/post/create/(Post object)`
* Display all groups -> `/api/post/all`
* Display posts from specific group -> `/api/post/(Group id)`

## Comment Controller
* Create comment -> `/api/comment/create/(Comment object)`
* Display all comments from a specific user -> `/api/comment/user/(User object)`
* Display comments on a post -> `/api/comment/post/(Post id)`
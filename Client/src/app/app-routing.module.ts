import { NgModule } from '@angular/core';
import { RegisterComponent } from './register/register.component';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './helpers/auth.guard';
import { RoleGuard } from './helpers/role.guard';
import { ManageUsersComponent } from './manage-users/manage-users.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { CreateGroupComponent } from './create-group/create-group.component';
import { GroupsComponent } from './groups/groups.component';
import { PostsComponent } from './posts/posts.component';
import { GroupDetailComponent } from './group-detail/group-detail.component';
import { DeletePostComponent } from './delete-post/delete-post.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'manageaccount', component: ManageUsersComponent, canActivate: [RoleGuard] },
  { path: 'userdetail/:id', component: UserDetailComponent, canActivate: [RoleGuard] },
  { path: 'creategroup', component: CreateGroupComponent, canActivate: [RoleGuard] },
  { path: 'groups', component: GroupsComponent, canActivate: [AuthGuard] },
  { path: 'groupDetail/:id', component: GroupDetailComponent, canActivate: [AuthGuard] },
  { path: 'posts', component: PostsComponent, canActivate: [AuthGuard] },
  { path: 'delete-post/:id', component: DeletePostComponent, canActivate: [RoleGuard] }

];
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

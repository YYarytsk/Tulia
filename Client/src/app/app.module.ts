import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegisterComponent } from './register/register.component';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { MatSliderModule } from '@angular/material/slider';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';
import { NavigationbarComponent } from './navigationbar/navigationbar.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { HomeComponent } from './home/home.component';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ManageUsersComponent } from './manage-users/manage-users.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { CreateGroupComponent } from './create-group/create-group.component';
import { GroupsComponent } from './groups/groups.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { FlexLayoutModule } from '@angular/flex-layout';
import { GroupDetailComponent } from './group-detail/group-detail.component';
import { PostsComponent } from './posts/posts.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { DeletePostComponent } from './delete-post/delete-post.component';


@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    NavigationbarComponent,
    HomeComponent,
    ManageUsersComponent,
    UserDetailComponent,
    CreateGroupComponent,
    GroupsComponent,
    GroupDetailComponent,
    PostsComponent,
    DeletePostComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    MatSliderModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatRadioModule,
    MatCardModule,
    LayoutModule,
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatGridListModule,
    FlexLayoutModule,
    MatCheckboxModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

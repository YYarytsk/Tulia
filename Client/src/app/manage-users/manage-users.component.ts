import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { User } from '../models/user';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-manage-users',
  templateUrl: './manage-users.component.html',
  styleUrls: ['./manage-users.component.css']
})
export class ManageUsersComponent implements OnInit {

  users: User[] = [];
  user?: User;
  constructor(
    private userService: UserService, 
    private route: ActivatedRoute,
    private location: Location) { }

  ngOnInit(): void {
    this.getUser();
    this.getUsers();
    this.save();
  }

  getUser(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.userService.getUser(id)
      .subscribe(user => this.user = user);
  }

  getUsers(): void{
    this.userService.getUsers()
      .subscribe(users => this.users = users)
  }

  save(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (this.user && id) {
      this.userService.updateUser(id, this.user)
        .subscribe(() => this.goBack());
    }
  }


  goBack(): void {
    this.location.back();
  }

}

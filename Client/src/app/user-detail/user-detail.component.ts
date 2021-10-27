import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';
import { User } from '../models/user';
import { Location } from '@angular/common';
import { UserDetail } from '../models/userdetail';
@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {

  user!: User;
  userdetail!: UserDetail;
  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getUser();
  }
  getUser(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.userService.getUser(id)
      .subscribe(user => this.user = user);
  }

  save(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (this.user && id) {
      this.userService.updateUser(id, this.user)
        .subscribe(() => this.goBack());
    }
  }

  delete(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    //this.user = this.user.filter(u => u !== user);
    this.userService.deleteUser(id).subscribe(() => this.goBack());
  }

  goBack(): void {
    this.location.back();
  }

}

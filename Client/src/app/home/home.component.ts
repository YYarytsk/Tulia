import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';
import { User } from '../models/user';
import { UserDetail } from '../models/userdetail';
import { GroupService } from '../group.service';
import { Membership } from '../models/membership';
import { Group } from '../models/group';
import { newArray } from '@angular/compiler/src/util';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @Input() user!: User;
  userdetail!: UserDetail;
  member!: Membership;
  memberships: Membership[] = [];
  groups: Group[] = [];
  group!: Group;
  groupTitles: any;
  groupIds: any;
  Ids: any;
  //user!: User;
  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private groupService: GroupService
  ) {
    this.user = this.userService.userValue;
  }

  getUserwithGroup() {
    this.groupTitles = new Array();
    this.groupIds = new Array();
    this.Ids = new Array();
    console.log(this.Ids);
    console.log(this.groupTitles);
    this.userService.getUserwithGroup(this.user.id)
      .subscribe(
        userdetail => {
          this.userdetail = userdetail;
          this.groups = this.userdetail.groups;
          this.memberships = this.userdetail?.memberships;
          //console.log(this.userdetail?.memberships);

          for (let i = 0; i < this.memberships.length; i++) {
            this.groupIds.push(this.memberships[i].groupId);
          }
          for (let groupId of this.groupIds) {
            this.groupService.getGroupById(groupId).subscribe(group => {
              this.group = group,
                this.groupTitles.push(groupId + this.group.groupTitle),
                this.Ids.push(groupId)
            })
          }

        }
      );


  }

  getUser(): void {

    this.userService.getUser(this.user.id)
      .subscribe(user => this.user = user);
  }


  ngOnInit(): void {

    this.getUserwithGroup();
    //this.getUserwithGroup();
    //this.getMember();
  }

}





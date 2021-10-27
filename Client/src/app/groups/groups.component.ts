import { Component, OnInit } from '@angular/core';
import { GroupService } from '../group.service';
import { Group } from '../models/group';
import { User } from '../models/user';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {
  user!: User;
  group!: Group;
  id: number | any;
  groups: Group[] = [];
  submitted = false;
  loading = false;

  form: FormGroup = new FormGroup({

    userId: new FormControl(''),
    groupId: new FormControl('')
  });

  constructor(
    private groupService: GroupService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public userService: UserService
  ) { this.user = this.userService.userValue; }

  ngOnInit(): void {
    this.getallgroups();
    this.form = this.formBuilder.group({
      //userId: ['', Validators.required],
      userId: [this.user.id],
      groupId: ['', Validators.required]
    })
  };

  getallgroups(): void {
    this.groupService.getallGroups()
      .subscribe(groups => this.groups = groups);
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }
  //when they hit join, +1 to NumberGroups, +1 to NumberMember, and create new Membership
  onSubmit() {

    this.submitted = true;
    //stop here if form is invalid
    if (this.form.invalid) {
      return;
    }
    this.loading = true;

    this.groupService.CreateMembership(this.form.value)
      .pipe(first())
      .subscribe(
        data => {
          console.log(this.form.value);
          this.groupService.updateGroup(this.form.value.groupId, this.group).subscribe(data => { console.log("membernumber +1") });
          this.userService.updateUser(this.user.id, this.user).subscribe(
            data => {
              console.log("groupnumber +1");
              this.router.navigate(['../groupDetail/' + this.form.value.groupId], { relativeTo: this.route });
              alert("Joined successfully!");
            },
            error => {
              //groupnumber<40
              if (this.user.numberGroups > 3) {
                this.loading = false;
                alert("you can only join total of 3 groups!");
              }
            }
          );
        },
        error => {
          this.loading = false;
          alert(error);
        }
      );


  }


}

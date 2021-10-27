import { Component, OnInit } from '@angular/core';
import { Group } from '../models/group';
import { GroupService } from '../group.service';
import { UserService } from '../user.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { User } from '../models/user';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.css']
})
export class CreateGroupComponent implements OnInit {

  user!: User;
  //userId = this.userService.currentUserid;
  loading = false;
  submitted = false;

  form: FormGroup = new FormGroup({

    userId: new FormControl(''),
    groupTitle: new FormControl(''),
    description: new FormControl('')

  });

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private groupService: GroupService,
    public userService: UserService
  ) { this.user = this.userService.userValue; }

  ngOnInit() {
    this.form = this.formBuilder.group({
      //userId: ['', Validators.required],
      userId: [this.user.id],
      groupTitle: ['', Validators.required],
      description: ['', Validators.required]
    });
  }
  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
    console.log(this.form);
    console.log(this.userService.user.id);
    this.submitted = true;
    //stop here if form is invalid
    if (this.form.invalid) {
      return;
    }
    this.loading = true;
    this.groupService.createGroup(this.form.value)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate(['../groups'], { relativeTo: this.route });
          alert("create successfully!");
        },
        error => {
          this.loading = false;
          alert(error);
        }
      )
  }

}

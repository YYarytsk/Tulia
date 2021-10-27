import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from '../models/user';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  loading = false;
  submitted = false;

  form: FormGroup = new FormGroup({

    firstName: new FormControl(''),
    lastName: new FormControl(''),
    username: new FormControl(''),
    password: new FormControl(''),

  });

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService
    ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      username: ['', [Validators.required, Validators.minLength(5)]],
      password: ['', [Validators.required, Validators.minLength(6)]]
  });
  }
  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;
     //stop here if form is invalid
    if (this.form.invalid) {
        return;
    }
    this.loading = true;
    this.userService.addUser(this.form.value)
        .pipe(first())
        .subscribe(
          data => {
            this.router.navigate(['../login'], {relativeTo: this.route});
            alert("Register successfully!");
          },
          error => {
            this.loading = false;
            alert(error);
          }
        )
  }

}

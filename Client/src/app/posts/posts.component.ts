import { Component, Input, OnInit } from '@angular/core';
import { GroupService } from '../group.service';
import { Group } from '../models/group';
import { Post } from '../models/post';
import { PostsService } from '../posts.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { User } from '../models/user';
import { UserService } from '../user.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  post!: Post;
  user!: User;
  time: string = new Date().toISOString().slice(0, 19).replace('T', ' ');
  //@Input() createdPost: Post = { body: "", userId: 1, title: "", groupId: 1 }

  posts: Post[] = [];
  groups: Group[] = [];
  submitted = false;
  loading = false;

  form: FormGroup = new FormGroup({

    userId: new FormControl(''),
    postId: new FormControl(''),
    content: new FormControl('')

  });
  constructor(
    private postService: PostsService,
    private groupService: GroupService,
    private userService: UserService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute
  ) { this.user = this.userService.userValue; }

  ngOnInit(): void {
    this.postService.getAllPosts(this.post).subscribe(posts => this.posts = posts);

    this.form = this.formBuilder.group({
      userId: [this.user.id],
      postId: [''],
      content: ['', [Validators.required]]

    });
  }

  submitPost(): void {
    this.postService.createPost(this.post);
  }

  getAllGroups(): void {
    this.groupService.getallGroups().subscribe(groups => this.groups = groups);
  }



  AddComment() {

    this.submitted = true;
    //stop here if form is invalid
    if (this.form.invalid) {
      return;
    }
    this.loading = true;
    this.postService.createComment(this.form.value)
      .pipe(first())
      .subscribe(
        data => {
          const id = Number(this.route.snapshot.paramMap.get('id'));
          this.router.navigate(['../groupDetail/' + id], { relativeTo: this.route });
          console.log("added comment");
        },
        error => {
          this.loading = false;
          alert(error);
        }
      )
  }

}
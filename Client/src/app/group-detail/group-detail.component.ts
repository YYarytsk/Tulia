import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupService } from '../group.service';
import { Group } from '../models/group';
import { UserService } from '../user.service';
import { PostsService } from '../posts.service';
import { User } from '../models/user';
import { first } from 'rxjs/operators';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Post } from '../models/post';
import { PostDetail } from '../models/postdetail';
@Component({
  selector: 'app-group-detail',
  templateUrl: './group-detail.component.html',
  styleUrls: ['./group-detail.component.css']
})
export class GroupDetailComponent implements OnInit {
  user!: User;
  post!: PostDetail;
  group!: Group;
  submitted = false;
  loading = false;
  loading2 = false;
  groups: Group[] = [];
  posts: PostDetail[] = [];
  postdetail!: PostDetail;
  postIds: any;
  comments: any;
  groupid = Number(this.route.snapshot.paramMap.get('id'));

  postForm: FormGroup = new FormGroup({
    userId: new FormControl(''),
    groupId: new FormControl(''),
    title: new FormControl(''),
    body: new FormControl('')

  })
  form: FormGroup = new FormGroup({

    userId: new FormControl(''),
    postId: new FormControl(''),
    content: new FormControl('')

  });
  constructor(
    private postService: PostsService,
    private groupService: GroupService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private userService: UserService) { this.user = this.userService.userValue; }

  ngOnInit(): void {

    this.getGroup();
    this.form = this.formBuilder.group({
      userId: [this.user.id],
      postId: ['', Validators.required],
      content: ['', [Validators.required]]
    });
    this.postForm = this.formBuilder.group({
      userId: [this.user.id],
      groupId: [this.groupid],
      title: ['', Validators.required],
      body: ['', Validators.required]
    });
  }

  submitPost(): void {
    this.postService.createPost(this.post);
  }

  //delete membership, NumGroups of user -1, MemberNumber of Group -1
  leaveGroup(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.groupService.deleteMembership(this.user.id, id);
    this.groupService.updateGroupWhenLeave(id, this.group);
    this.userService.updateUserWhenLeave(id, this.user)
  }

  getGroup(): void {
    //this.postIds = new Array();
    //this.comments = new Array();
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.groupService.getGroupIncludingPosts(id)
      .subscribe(
        group => {
          this.group = group;
          this.posts = this.group.posts
          for (let postdetail of this.group.posts) {
            this.comments = new Array();
            this.postService.getPostById(postdetail.id).subscribe(post => {
              this.post = post;
              this.comments.push(this.post.comments)
            })
            //this.postIds.push(postdetail.id)
            console.log(this.postIds);
            console.log(this.comments)
          }
          //for (let postId of this.postIds) {
          //  this.postService.getPostById(postId).subscribe(post => {
          //    this.post = post;
          //    for (let comment of this.post.comments) {
          //      this.comments.push(comment)
          //    }
          //    console.log(post.comments)
          //  })
          //}
        }

      );
  }
  AddPost() {
    //this.submitted = true;
    //stop here if form is invalid
    if (this.postForm.invalid) {
      return;
    }
    this.loading2 = true;
    this.postService.createPost(this.postForm.value)
      .pipe(first())
      .subscribe(
        data => {
          const id = Number(this.route.snapshot.paramMap.get('id'));
          this.router.navigate(['../' + id], { relativeTo: this.route });
          console.log("added post");
        },
        error => {
          this.loading2 = false;
          alert(error);
        }
      )
  }


  onSubmit() {
    this.submitted = true;
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.groupService.updateGroupWhenLeave(id, this.group).subscribe(data => { console.log("membernumber -1") });
    this.userService.updateUserWhenLeave(this.user.id, this.user).subscribe(data => { console.log("groupnumber -1") });
    this.groupService.deleteMembership(this.user.id, id).subscribe(
      confirm => {
        this.router.navigate(['../../'], { relativeTo: this.route });
        alert("you left this group!");
      }
    )
  }
  get f() { return this.form.controls; }
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
          this.router.navigate(['../' + id], { relativeTo: this.route });
          console.log("added comment");
        },
        error => {
          this.loading = false;
          alert(error);
        }
      )
  }


}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostDetail } from '../models/postdetail';
import { PostsService } from '../posts.service';
import { Location } from '@angular/common';
@Component({
  selector: 'app-delete-post',
  templateUrl: './delete-post.component.html',
  styleUrls: ['./delete-post.component.css']
})
export class DeletePostComponent implements OnInit {
  post!: PostDetail;
  constructor(
    private postService: PostsService,
    private route: ActivatedRoute,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getPost();
  }

  getPost(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.postService.getPostById(id)
      .subscribe(post => this.post = post);
  }
  delete(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.postService.deletePost(id).subscribe(() => this.goBack());
  }
  goBack(): void {
    this.location.back();
  }
}

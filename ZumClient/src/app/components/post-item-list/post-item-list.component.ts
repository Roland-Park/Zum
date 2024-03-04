import { Observable } from 'rxjs';
import { IPost } from 'src/app/models/post';
import { PostService } from './../../services/postService/post.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'post-item-list',
  templateUrl: './post-item-list.component.html',
  styleUrls: ['./post-item-list.component.css']
})
export class PostItemListComponent implements OnInit {
  posts$: Observable<IPost[]>;
  isLoading$: Observable<boolean>;


  constructor(private postService: PostService) {}
  ngOnInit(): void {
    this.isLoading$ = this.postService.isLoading$;
    this.posts$ = this.postService.posts$;
    this.postService.loadPosts("tech");
  }
}

import { Component, Input } from '@angular/core';
import { IPost } from 'src/app/models/post';

@Component({
  selector: 'post-item',
  templateUrl: './post-item.component.html',
  styleUrls: ['./post-item.component.css']
})
export class PostItemComponent {
  @Input() post: IPost;
}

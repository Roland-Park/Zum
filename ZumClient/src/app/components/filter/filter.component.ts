import { ToastService } from 'src/app/services/toastService/toast.service';
import { PostService } from './../../services/postService/post.service';
import { Component } from '@angular/core';

@Component({
  selector: 'filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css']
})
export class FilterComponent {
  tagCsv: string;
  sortBy: string; 
  direction: string;

  constructor(private postService: PostService, private toast: ToastService){}

  filter(){
    if(!this.tagCsv){
      this.toast.notifyError("tags parameter is required");
      return;
    } 

    if((this.sortBy && !this.direction) ||
        (this.direction && !this.sortBy)){
          this.toast.notifyError("'sort by' and 'direction' must both be set.");
          return;
    } 

    if(!this.sortBy && !this.direction){
      this.postService.loadPosts(this.tagCsv);
    }
    else if(this.sortBy && this.direction){
      this.postService.loadPosts(this.tagCsv, this.sortBy, this.direction);
    }
  }
}

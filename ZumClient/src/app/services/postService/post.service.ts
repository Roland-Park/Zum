import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, shareReplay, tap, throwError } from 'rxjs';
import { environment } from 'src/app/environment';
import { IPost } from 'src/app/models/post';
import { ToastService } from '../toastService/toast.service';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  protected isLoadingSubject = new BehaviorSubject<boolean>(false);
  isLoading$ = this.isLoadingSubject.asObservable();

  private postsSubject = new BehaviorSubject<IPost[]>([]);
  posts$ = this.postsSubject.asObservable();

  constructor(private http: HttpClient, private toast: ToastService) { }

  loadPosts(tagsCsv: string, sortBy: string | null = null, direction: string | null = null){
    if(!tagsCsv){
      this.toast.notifyError("Tags parameter is required");
    }
    this.isLoadingSubject.next(true);
    return this.http.get<IPost[]>(this.buildUrlQueryString(tagsCsv, sortBy, direction))
    .pipe(
      catchError(err => {
        this.postsSubject.next([]);
        return this.handleError(err);
      }),
      tap(posts => {
        this.postsSubject.next(posts);
        this.isLoadingSubject.next(false);
      }),
      shareReplay()
    ).subscribe();
  };

  private handleError(error: HttpErrorResponse){
    if(error.status != 404){
      this.toast.notifyError("We're having problems getting data. Check your internet connection.");
    }
    this.isLoadingSubject.next(false);
    return throwError(() => error);
  };

  private buildUrlQueryString(tagsCsv: string, sortBy: string | null, direction: string | null): string{
    if(!sortBy || !direction){
      return `${environment.postServiceUrlRoot}?tags=${tagsCsv}`;
    }

    return `${environment.postServiceUrlRoot}?tags=${tagsCsv}&sortBy=${sortBy}&direction=${direction}`;
  }
}

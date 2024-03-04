import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, shareReplay, tap, throwError } from 'rxjs';
import { environment } from 'src/app/environment';
import { IPost } from 'src/app/models/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  protected isLoadingSubject = new BehaviorSubject<boolean>(false);
  isLoading$ = this.isLoadingSubject.asObservable();

  private postsSubject = new BehaviorSubject<IPost[]>([]);
  posts$ = this.postsSubject.asObservable();

  constructor(private http: HttpClient) { }

  loadPosts(tagsCsv: string, sortBy: string | null, direction: string | null){
    this.isLoadingSubject.next(true);
    return this.http.get<IPost[]>(`${environment.postServiceUrlRoot}`)
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
    );
  }
  handleError(error: HttpErrorResponse){
    this.isLoadingSubject.next(false);
    return throwError(() => error);
  }
}

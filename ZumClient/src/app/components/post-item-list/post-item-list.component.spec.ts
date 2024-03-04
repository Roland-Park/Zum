import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostItemListComponent } from './post-item-list.component';

describe('PostItemListComponent', () => {
  let component: PostItemListComponent;
  let fixture: ComponentFixture<PostItemListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PostItemListComponent]
    });
    fixture = TestBed.createComponent(PostItemListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

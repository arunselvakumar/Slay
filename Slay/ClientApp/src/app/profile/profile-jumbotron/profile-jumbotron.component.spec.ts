import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileJumbotronComponent } from './profile-jumbotron.component';

describe('ProfileJumbotronComponent', () => {
  let component: ProfileJumbotronComponent;
  let fixture: ComponentFixture<ProfileJumbotronComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfileJumbotronComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileJumbotronComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

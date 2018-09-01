import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MemePostComponentComponent } from './meme-post-component.component';

describe('MemePostComponentComponent', () => {
  let component: MemePostComponentComponent;
  let fixture: ComponentFixture<MemePostComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MemePostComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MemePostComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

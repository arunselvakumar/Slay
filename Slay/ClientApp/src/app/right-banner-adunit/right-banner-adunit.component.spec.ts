import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RightBannerAdunitComponent } from './right-banner-adunit.component';

describe('RightBannerAdunitComponent', () => {
  let component: RightBannerAdunitComponent;
  let fixture: ComponentFixture<RightBannerAdunitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RightBannerAdunitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RightBannerAdunitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

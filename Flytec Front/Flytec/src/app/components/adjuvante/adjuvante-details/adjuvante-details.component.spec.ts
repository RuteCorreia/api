import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjuvanteDetailsComponent } from './adjuvante-details.component';

describe('AdjuvanteDetailsComponent', () => {
  let component: AdjuvanteDetailsComponent;
  let fixture: ComponentFixture<AdjuvanteDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdjuvanteDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdjuvanteDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

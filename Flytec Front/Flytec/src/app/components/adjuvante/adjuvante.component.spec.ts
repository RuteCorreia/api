import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjuvanteComponent } from './adjuvante.component';

describe('AdjuvanteComponent', () => {
  let component: AdjuvanteComponent;
  let fixture: ComponentFixture<AdjuvanteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdjuvanteComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdjuvanteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjuvanteListComponent } from './adjuvante-list.component';

describe('AdjuvanteListComponent', () => {
  let component: AdjuvanteListComponent;
  let fixture: ComponentFixture<AdjuvanteListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdjuvanteListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdjuvanteListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

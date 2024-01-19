import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulaDetailsComponent } from './bula-details.component';

describe('BulaDetailsComponent', () => {
  let component: BulaDetailsComponent;
  let fixture: ComponentFixture<BulaDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BulaDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BulaDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

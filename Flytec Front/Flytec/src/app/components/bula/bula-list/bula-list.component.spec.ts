import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulaListComponent } from './bula-list.component';

describe('BulaListComponent', () => {
  let component: BulaListComponent;
  let fixture: ComponentFixture<BulaListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BulaListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BulaListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PistaDetailsComponent } from './pista-details.component';

describe('PistaDetailsComponent', () => {
  let component: PistaDetailsComponent;
  let fixture: ComponentFixture<PistaDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PistaDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PistaDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

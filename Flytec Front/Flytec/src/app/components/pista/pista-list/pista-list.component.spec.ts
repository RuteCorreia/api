import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PistaListComponent } from './pista-list.component';

describe('PistaListComponent', () => {
  let component: PistaListComponent;
  let fixture: ComponentFixture<PistaListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PistaListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PistaListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

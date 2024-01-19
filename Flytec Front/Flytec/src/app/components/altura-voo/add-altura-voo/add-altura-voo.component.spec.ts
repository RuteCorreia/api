import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAlturaVooComponent } from './add-altura-voo.component';

describe('AddAlturaVooComponent', () => {
  let component: AddAlturaVooComponent;
  let fixture: ComponentFixture<AddAlturaVooComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAlturaVooComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAlturaVooComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

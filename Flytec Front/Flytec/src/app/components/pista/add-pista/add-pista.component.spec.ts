import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPistaComponent } from './add-pista.component';

describe('AddPistaComponent', () => {
  let component: AddPistaComponent;
  let fixture: ComponentFixture<AddPistaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddPistaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddPistaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

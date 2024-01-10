import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVeiculanteComponent } from './add-veiculante.component';

describe('AddVeiculanteComponent', () => {
  let component: AddVeiculanteComponent;
  let fixture: ComponentFixture<AddVeiculanteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddVeiculanteComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddVeiculanteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

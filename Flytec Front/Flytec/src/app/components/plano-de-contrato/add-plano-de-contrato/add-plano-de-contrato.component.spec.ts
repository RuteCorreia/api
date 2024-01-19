import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPlanoDeContratoComponent } from './add-plano-de-contrato.component';

describe('AddPlanoDeContratoComponent', () => {
  let component: AddPlanoDeContratoComponent;
  let fixture: ComponentFixture<AddPlanoDeContratoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddPlanoDeContratoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddPlanoDeContratoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

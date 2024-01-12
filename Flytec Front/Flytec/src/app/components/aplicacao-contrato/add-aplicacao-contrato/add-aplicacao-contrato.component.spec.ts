import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAplicacaoContratoComponent } from './add-aplicacao-contrato.component';

describe('AddAplicacaoContratoComponent', () => {
  let component: AddAplicacaoContratoComponent;
  let fixture: ComponentFixture<AddAplicacaoContratoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAplicacaoContratoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAplicacaoContratoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

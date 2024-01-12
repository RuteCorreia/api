import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoContratoComponent } from './aplicacao-contrato.component';

describe('AplicacaoContratoComponent', () => {
  let component: AplicacaoContratoComponent;
  let fixture: ComponentFixture<AplicacaoContratoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AplicacaoContratoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AplicacaoContratoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

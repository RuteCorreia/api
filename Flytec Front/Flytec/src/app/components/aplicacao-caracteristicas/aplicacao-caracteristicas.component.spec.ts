import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoCaracteristicasComponent } from './aplicacao-caracteristicas.component';

describe('AplicacaoCaracteristicasComponent', () => {
  let component: AplicacaoCaracteristicasComponent;
  let fixture: ComponentFixture<AplicacaoCaracteristicasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AplicacaoCaracteristicasComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AplicacaoCaracteristicasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAplicacaoCaracteristicasComponent } from './add-aplicacao-caracteristicas.component';

describe('AddAplicacaoCaracteristicasComponent', () => {
  let component: AddAplicacaoCaracteristicasComponent;
  let fixture: ComponentFixture<AddAplicacaoCaracteristicasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAplicacaoCaracteristicasComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAplicacaoCaracteristicasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanoDeContratoComponent } from './plano-de-contrato.component';

describe('PlanoDeContratoComponent', () => {
  let component: PlanoDeContratoComponent;
  let fixture: ComponentFixture<PlanoDeContratoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PlanoDeContratoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PlanoDeContratoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

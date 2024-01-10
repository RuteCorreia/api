import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoCroquiImportacaoComponent } from './aplicacao-croqui-importacao.component';

describe('AplicacaoCroquiImportacaoComponent', () => {
  let component: AplicacaoCroquiImportacaoComponent;
  let fixture: ComponentFixture<AplicacaoCroquiImportacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AplicacaoCroquiImportacaoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AplicacaoCroquiImportacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

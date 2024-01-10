import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoRecomendacoesTecnicasComponent } from './aplicacao-recomendacoes-tecnicas.component';

describe('AplicacaoRecomendacoesTecnicasComponent', () => {
  let component: AplicacaoRecomendacoesTecnicasComponent;
  let fixture: ComponentFixture<AplicacaoRecomendacoesTecnicasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AplicacaoRecomendacoesTecnicasComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AplicacaoRecomendacoesTecnicasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

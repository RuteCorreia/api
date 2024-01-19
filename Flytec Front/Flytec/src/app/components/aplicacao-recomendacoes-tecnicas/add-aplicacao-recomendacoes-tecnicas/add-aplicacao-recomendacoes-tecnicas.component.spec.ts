import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAplicacaoRecomendacoesTecnicasComponent } from './add-aplicacao-recomendacoes-tecnicas.component';

describe('AddAplicacaoRecomendacoesTecnicasComponent', () => {
  let component: AddAplicacaoRecomendacoesTecnicasComponent;
  let fixture: ComponentFixture<AddAplicacaoRecomendacoesTecnicasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAplicacaoRecomendacoesTecnicasComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAplicacaoRecomendacoesTecnicasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

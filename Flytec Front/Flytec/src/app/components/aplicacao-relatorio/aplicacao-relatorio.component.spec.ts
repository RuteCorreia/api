import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoRelatorioComponent } from './aplicacao-relatorio.component';

describe('AplicacaoRelatorioComponent', () => {
  let component: AplicacaoRelatorioComponent;
  let fixture: ComponentFixture<AplicacaoRelatorioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AplicacaoRelatorioComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AplicacaoRelatorioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoRelatorioItemComponent } from './aplicacao-relatorio-item.component';

describe('AplicacaoRelatorioItemComponent', () => {
  let component: AplicacaoRelatorioItemComponent;
  let fixture: ComponentFixture<AplicacaoRelatorioItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AplicacaoRelatorioItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AplicacaoRelatorioItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

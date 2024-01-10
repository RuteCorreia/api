import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAplicacaoRelatorioComponent } from './add-aplicacao-relatorio.component';

describe('AddAplicacaoRelatorioComponent', () => {
  let component: AddAplicacaoRelatorioComponent;
  let fixture: ComponentFixture<AddAplicacaoRelatorioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAplicacaoRelatorioComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAplicacaoRelatorioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

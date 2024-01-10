import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAplicacaoCroquiImportacaoComponent } from './add-aplicacao-croqui-importacao.component';

describe('AddAplicacaoCroquiImportacaoComponent', () => {
  let component: AddAplicacaoCroquiImportacaoComponent;
  let fixture: ComponentFixture<AddAplicacaoCroquiImportacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAplicacaoCroquiImportacaoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAplicacaoCroquiImportacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

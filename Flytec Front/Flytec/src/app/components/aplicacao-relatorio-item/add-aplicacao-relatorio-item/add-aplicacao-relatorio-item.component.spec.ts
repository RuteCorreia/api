import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAplicacaoRelatorioItemComponent } from './add-aplicacao-relatorio-item.component';

describe('AddAplicacaoRelatorioItemComponent', () => {
  let component: AddAplicacaoRelatorioItemComponent;
  let fixture: ComponentFixture<AddAplicacaoRelatorioItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAplicacaoRelatorioItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAplicacaoRelatorioItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

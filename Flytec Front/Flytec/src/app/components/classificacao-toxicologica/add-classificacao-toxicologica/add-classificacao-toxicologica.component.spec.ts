import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddClassificacaoToxicologicaComponent } from './add-classificacao-toxicologica.component';

describe('AddClassificacaoToxicologicaComponent', () => {
  let component: AddClassificacaoToxicologicaComponent;
  let fixture: ComponentFixture<AddClassificacaoToxicologicaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddClassificacaoToxicologicaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddClassificacaoToxicologicaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

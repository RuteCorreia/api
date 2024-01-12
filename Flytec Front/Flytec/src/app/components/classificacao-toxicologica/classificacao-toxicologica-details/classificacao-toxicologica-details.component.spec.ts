import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassificacaoToxicologicaDetailsComponent } from './classificacao-toxicologica-details.component';

describe('ClassificacaoToxicologicaDetailsComponent', () => {
  let component: ClassificacaoToxicologicaDetailsComponent;
  let fixture: ComponentFixture<ClassificacaoToxicologicaDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ClassificacaoToxicologicaDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClassificacaoToxicologicaDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

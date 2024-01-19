import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassificacaoToxicologicaListComponent } from './classificacao-toxicologica-list.component';

describe('ClassificacaoToxicologicaListComponent', () => {
  let component: ClassificacaoToxicologicaListComponent;
  let fixture: ComponentFixture<ClassificacaoToxicologicaListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ClassificacaoToxicologicaListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClassificacaoToxicologicaListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

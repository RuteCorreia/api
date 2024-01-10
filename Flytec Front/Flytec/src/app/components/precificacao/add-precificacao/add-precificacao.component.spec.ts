import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPrecificacaoComponent } from './add-precificacao.component';

describe('AddPrecificacaoComponent', () => {
  let component: AddPrecificacaoComponent;
  let fixture: ComponentFixture<AddPrecificacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddPrecificacaoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddPrecificacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

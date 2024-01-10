import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAplicacaoComponent } from './add-aplicacao.component';

describe('AddAplicacaoComponent', () => {
  let component: AddAplicacaoComponent;
  let fixture: ComponentFixture<AddAplicacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAplicacaoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAplicacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

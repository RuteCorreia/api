import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAplicacaoLogComponent } from './add-aplicacao-log.component';

describe('AddAplicacaoLogComponent', () => {
  let component: AddAplicacaoLogComponent;
  let fixture: ComponentFixture<AddAplicacaoLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAplicacaoLogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAplicacaoLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAplicacaoCroquiComponent } from './add-aplicacao-croqui.component';

describe('AddAplicacaoCroquiComponent', () => {
  let component: AddAplicacaoCroquiComponent;
  let fixture: ComponentFixture<AddAplicacaoCroquiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAplicacaoCroquiComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAplicacaoCroquiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

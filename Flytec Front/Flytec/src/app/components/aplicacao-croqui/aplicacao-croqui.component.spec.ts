import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoCroquiComponent } from './aplicacao-croqui.component';

describe('AplicacaoCroquiComponent', () => {
  let component: AplicacaoCroquiComponent;
  let fixture: ComponentFixture<AplicacaoCroquiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AplicacaoCroquiComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AplicacaoCroquiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoLogComponent } from './aplicacao-log.component';

describe('AplicacaoLogComponent', () => {
  let component: AplicacaoLogComponent;
  let fixture: ComponentFixture<AplicacaoLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AplicacaoLogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AplicacaoLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

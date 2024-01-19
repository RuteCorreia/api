import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoDetailsComponent } from './aplicacao-details.component';

describe('AplicacaoDetailsComponent', () => {
  let component: AplicacaoDetailsComponent;
  let fixture: ComponentFixture<AplicacaoDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AplicacaoDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AplicacaoDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

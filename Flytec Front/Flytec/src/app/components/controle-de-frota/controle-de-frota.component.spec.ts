import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ControleDeFrotaComponent } from './controle-de-frota.component';

describe('ControleDeFrotaComponent', () => {
  let component: ControleDeFrotaComponent;
  let fixture: ComponentFixture<ControleDeFrotaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ControleDeFrotaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ControleDeFrotaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

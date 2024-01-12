import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VeiculanteComponent } from './veiculante.component';

describe('VeiculanteComponent', () => {
  let component: VeiculanteComponent;
  let fixture: ComponentFixture<VeiculanteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VeiculanteComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VeiculanteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

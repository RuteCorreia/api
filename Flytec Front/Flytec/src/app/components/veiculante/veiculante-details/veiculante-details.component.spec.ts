import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VeiculanteDetailsComponent } from './veiculante-details.component';

describe('VeiculanteDetailsComponent', () => {
  let component: VeiculanteDetailsComponent;
  let fixture: ComponentFixture<VeiculanteDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VeiculanteDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VeiculanteDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

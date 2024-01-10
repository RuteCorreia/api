import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombateIncendioDecolagemPousoComponent } from './combate-incendio-decolagem-pouso.component';

describe('CombateIncendioDecolagemPousoComponent', () => {
  let component: CombateIncendioDecolagemPousoComponent;
  let fixture: ComponentFixture<CombateIncendioDecolagemPousoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CombateIncendioDecolagemPousoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CombateIncendioDecolagemPousoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

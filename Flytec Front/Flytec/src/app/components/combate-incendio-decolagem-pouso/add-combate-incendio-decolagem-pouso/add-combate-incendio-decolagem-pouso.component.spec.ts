import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCombateIncendioDecolagemPousoComponent } from './add-combate-incendio-decolagem-pouso.component';

describe('AddCombateIncendioDecolagemPousoComponent', () => {
  let component: AddCombateIncendioDecolagemPousoComponent;
  let fixture: ComponentFixture<AddCombateIncendioDecolagemPousoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddCombateIncendioDecolagemPousoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddCombateIncendioDecolagemPousoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

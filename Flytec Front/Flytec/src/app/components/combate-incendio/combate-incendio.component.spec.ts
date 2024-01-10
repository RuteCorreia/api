import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombateIncendioComponent } from './combate-incendio.component';

describe('CombateIncendioComponent', () => {
  let component: CombateIncendioComponent;
  let fixture: ComponentFixture<CombateIncendioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CombateIncendioComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CombateIncendioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

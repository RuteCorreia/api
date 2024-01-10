import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCombateIncendioComponent } from './add-combate-incendio.component';

describe('AddCombateIncendioComponent', () => {
  let component: AddCombateIncendioComponent;
  let fixture: ComponentFixture<AddCombateIncendioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddCombateIncendioComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddCombateIncendioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

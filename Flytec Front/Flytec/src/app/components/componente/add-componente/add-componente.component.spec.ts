import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddComponenteComponent } from './add-componente.component';

describe('AddComponenteComponent', () => {
  let component: AddComponenteComponent;
  let fixture: ComponentFixture<AddComponenteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddComponenteComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddComponenteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

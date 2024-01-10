import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEstadosComponent } from './add-estados.component';

describe('AddEstadosComponent', () => {
  let component: AddEstadosComponent;
  let fixture: ComponentFixture<AddEstadosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddEstadosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddEstadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

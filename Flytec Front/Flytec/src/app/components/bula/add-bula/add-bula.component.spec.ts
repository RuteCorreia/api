import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBulaComponent } from './add-bula.component';

describe('AddBulaComponent', () => {
  let component: AddBulaComponent;
  let fixture: ComponentFixture<AddBulaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddBulaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddBulaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

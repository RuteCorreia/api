import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCombustivelComponent } from './add-combustivel.component';

describe('AddCombustivelComponent', () => {
  let component: AddCombustivelComponent;
  let fixture: ComponentFixture<AddCombustivelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddCombustivelComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddCombustivelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

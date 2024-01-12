import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCulturaComponent } from './add-cultura.component';

describe('AddCulturaComponent', () => {
  let component: AddCulturaComponent;
  let fixture: ComponentFixture<AddCulturaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddCulturaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddCulturaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

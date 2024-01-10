import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFrotaComponent } from './add-frota.component';

describe('AddFrotaComponent', () => {
  let component: AddFrotaComponent;
  let fixture: ComponentFixture<AddFrotaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddFrotaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddFrotaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

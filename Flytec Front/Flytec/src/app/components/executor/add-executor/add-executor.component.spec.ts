import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddExecutorComponent } from './add-executor.component';

describe('AddExecutorComponent', () => {
  let component: AddExecutorComponent;
  let fixture: ComponentFixture<AddExecutorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddExecutorComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddExecutorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

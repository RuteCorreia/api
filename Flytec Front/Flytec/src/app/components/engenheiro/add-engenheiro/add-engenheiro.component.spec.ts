import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEngenheiroComponent } from './add-engenheiro.component';

describe('AddEngenheiroComponent', () => {
  let component: AddEngenheiroComponent;
  let fixture: ComponentFixture<AddEngenheiroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddEngenheiroComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddEngenheiroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

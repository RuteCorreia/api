import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEquipamentoComponent } from './add-equipamento.component';

describe('AddEquipamentoComponent', () => {
  let component: AddEquipamentoComponent;
  let fixture: ComponentFixture<AddEquipamentoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddEquipamentoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddEquipamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

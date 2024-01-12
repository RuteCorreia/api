import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipamentoDetailsComponent } from './equipamento-details.component';

describe('EquipamentoDetailsComponent', () => {
  let component: EquipamentoDetailsComponent;
  let fixture: ComponentFixture<EquipamentoDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EquipamentoDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EquipamentoDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VeiculanteListComponent } from './veiculante-list.component';

describe('VeiculanteListComponent', () => {
  let component: VeiculanteListComponent;
  let fixture: ComponentFixture<VeiculanteListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VeiculanteListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VeiculanteListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

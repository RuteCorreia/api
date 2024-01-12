import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpresaDetailsComponent } from './empresa-details.component';

describe('EmpresaDetailsComponent', () => {
  let component: EmpresaDetailsComponent;
  let fixture: ComponentFixture<EmpresaDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpresaDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpresaDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

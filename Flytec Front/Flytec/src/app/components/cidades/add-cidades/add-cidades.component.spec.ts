import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCidadesComponent } from './add-cidades.component';

describe('AddCidadesComponent', () => {
  let component: AddCidadesComponent;
  let fixture: ComponentFixture<AddCidadesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddCidadesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddCidadesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

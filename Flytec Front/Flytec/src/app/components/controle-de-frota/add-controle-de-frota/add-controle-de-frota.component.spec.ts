import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddControleDeFrotaComponent } from './add-controle-de-frota.component';

describe('AddControleDeFrotaComponent', () => {
  let component: AddControleDeFrotaComponent;
  let fixture: ComponentFixture<AddControleDeFrotaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddControleDeFrotaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddControleDeFrotaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientesDetailsComponent } from './clientes-details.component';

describe('ClientesDetailsComponent', () => {
  let component: ClientesDetailsComponent;
  let fixture: ComponentFixture<ClientesDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ClientesDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ClientesDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

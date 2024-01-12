import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlturaVooComponent } from './altura-voo.component';

describe('AlturaVooComponent', () => {
  let component: AlturaVooComponent;
  let fixture: ComponentFixture<AlturaVooComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AlturaVooComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AlturaVooComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

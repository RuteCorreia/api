import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AeronaveDetailsComponent } from './aeronave-details.component';

describe('AeronaveDetailsComponent', () => {
  let component: AeronaveDetailsComponent;
  let fixture: ComponentFixture<AeronaveDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AeronaveDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AeronaveDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

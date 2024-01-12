import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AeronaveListComponent } from './aeronave-list.component';

describe('AeronaveListComponent', () => {
  let component: AeronaveListComponent;
  let fixture: ComponentFixture<AeronaveListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AeronaveListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AeronaveListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

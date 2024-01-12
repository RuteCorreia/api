import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EngenheiroComponent } from './engenheiro.component';

describe('EngenheiroComponent', () => {
  let component: EngenheiroComponent;
  let fixture: ComponentFixture<EngenheiroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EngenheiroComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EngenheiroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

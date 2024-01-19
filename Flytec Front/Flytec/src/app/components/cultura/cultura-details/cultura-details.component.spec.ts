import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CulturaDetailsComponent } from './cultura-details.component';

describe('CulturaDetailsComponent', () => {
  let component: CulturaDetailsComponent;
  let fixture: ComponentFixture<CulturaDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CulturaDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CulturaDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

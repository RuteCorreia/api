import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CulturaComponent } from './cultura.component';

describe('CulturaComponent', () => {
  let component: CulturaComponent;
  let fixture: ComponentFixture<CulturaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CulturaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CulturaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

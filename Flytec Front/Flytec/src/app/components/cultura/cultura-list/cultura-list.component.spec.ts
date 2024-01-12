import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CulturaListComponent } from './cultura-list.component';

describe('CulturaListComponent', () => {
  let component: CulturaListComponent;
  let fixture: ComponentFixture<CulturaListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CulturaListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CulturaListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

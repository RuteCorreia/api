import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComponenteDetailsComponent } from './componente-details.component';

describe('ComponenteDetailsComponent', () => {
  let component: ComponenteDetailsComponent;
  let fixture: ComponentFixture<ComponenteDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ComponenteDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ComponenteDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

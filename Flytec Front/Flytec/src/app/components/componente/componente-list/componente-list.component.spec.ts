import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComponenteListComponent } from './componente-list.component';

describe('ComponenteListComponent', () => {
  let component: ComponenteListComponent;
  let fixture: ComponentFixture<ComponenteListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ComponenteListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ComponenteListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

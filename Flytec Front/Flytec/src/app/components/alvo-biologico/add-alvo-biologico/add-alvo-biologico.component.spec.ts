import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAlvoBiologicoComponent } from './add-alvo-biologico.component';

describe('AddAlvoBiologicoComponent', () => {
  let component: AddAlvoBiologicoComponent;
  let fixture: ComponentFixture<AddAlvoBiologicoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAlvoBiologicoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAlvoBiologicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

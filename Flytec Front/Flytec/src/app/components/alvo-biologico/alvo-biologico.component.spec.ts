import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlvoBiologicoComponent } from './alvo-biologico.component';

describe('AlvoBiologicoComponent', () => {
  let component: AlvoBiologicoComponent;
  let fixture: ComponentFixture<AlvoBiologicoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AlvoBiologicoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AlvoBiologicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

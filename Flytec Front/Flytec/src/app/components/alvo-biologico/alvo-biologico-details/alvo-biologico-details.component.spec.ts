import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlvoBiologicoDetailsComponent } from './alvo-biologico-details.component';

describe('AlvoBiologicoDetailsComponent', () => {
  let component: AlvoBiologicoDetailsComponent;
  let fixture: ComponentFixture<AlvoBiologicoDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AlvoBiologicoDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AlvoBiologicoDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

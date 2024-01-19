import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlvoBiologicoListComponent } from './alvo-biologico-list.component';

describe('AlvoBiologicoListComponent', () => {
  let component: AlvoBiologicoListComponent;
  let fixture: ComponentFixture<AlvoBiologicoListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AlvoBiologicoListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AlvoBiologicoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

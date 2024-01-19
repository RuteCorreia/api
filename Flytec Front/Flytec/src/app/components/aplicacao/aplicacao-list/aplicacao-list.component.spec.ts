import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoListComponent } from './aplicacao-list.component';

describe('AplicacaoListComponent', () => {
  let component: AplicacaoListComponent;
  let fixture: ComponentFixture<AplicacaoListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AplicacaoListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AplicacaoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

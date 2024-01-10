import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicacaoAreaTratadaComponent } from './aplicacao-area-tratada.component';

describe('AplicacaoAreaTratadaComponent', () => {
  let component: AplicacaoAreaTratadaComponent;
  let fixture: ComponentFixture<AplicacaoAreaTratadaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AplicacaoAreaTratadaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AplicacaoAreaTratadaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAplicacaoAreaTratadaComponent } from './add-aplicacao-area-tratada.component';

describe('AddAplicacaoAreaTratadaComponent', () => {
  let component: AddAplicacaoAreaTratadaComponent;
  let fixture: ComponentFixture<AddAplicacaoAreaTratadaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddAplicacaoAreaTratadaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddAplicacaoAreaTratadaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

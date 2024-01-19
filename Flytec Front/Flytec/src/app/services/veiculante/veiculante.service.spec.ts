import { TestBed } from '@angular/core/testing';

import { VeiculanteService } from './veiculante.service';

describe('VeiculanteService', () => {
  let service: VeiculanteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VeiculanteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

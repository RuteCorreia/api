import { TestBed } from '@angular/core/testing';

import { AdjuvanteService } from './adjuvante.service';

describe('AdjuvanteService', () => {
  let service: AdjuvanteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AdjuvanteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { BulaService } from './bula.service';

describe('BulaService', () => {
  let service: BulaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BulaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

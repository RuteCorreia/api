import { TestBed } from '@angular/core/testing';

import { AeronaveService } from './aeronave.service';

describe('AeronaveService', () => {
  let service: AeronaveService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AeronaveService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

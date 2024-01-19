import { TestBed } from '@angular/core/testing';

import { AlvoBiologicoService } from './alvo-biologico.service';

describe('AlvoBiologicoService', () => {
  let service: AlvoBiologicoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AlvoBiologicoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { AplicacaoService } from './aplicacao.service';

describe('AplicacaoService', () => {
  let service: AplicacaoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AplicacaoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

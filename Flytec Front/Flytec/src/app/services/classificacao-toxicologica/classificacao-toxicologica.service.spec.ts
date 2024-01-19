import { TestBed } from '@angular/core/testing';

import { ClassificacaoToxicologicaService } from './classificacao-toxicologica.service';

describe('ClassificacaoToxicologicaService', () => {
  let service: ClassificacaoToxicologicaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClassificacaoToxicologicaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { FormGeneratorService } from './form-generator.service';

describe('FormGeneratorService', () => {
  let service: FormGeneratorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormGeneratorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

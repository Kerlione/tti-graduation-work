import { TestBed } from '@angular/core/testing';

import { StepFormService } from './step-form.service';

describe('StepFormService', () => {
  let service: StepFormService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StepFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

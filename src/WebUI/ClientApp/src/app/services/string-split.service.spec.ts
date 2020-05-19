import { TestBed } from '@angular/core/testing';

import { StringSplitService } from './string-split.service';

describe('StringSplitService', () => {
  let service: StringSplitService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StringSplitService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

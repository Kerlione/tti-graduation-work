import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StringSplitService {

  constructor() { }

  public split(text: string): string {
    return text.match(/[A-Z][a-z]+|[0-9]+/g).join(' ');
  }
}

import { Injectable } from '@angular/core';
import { UserRole } from '../models/user-role';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }

  /**
   * storeToken
token:string   */
  public storeToken(token: string) {
    localStorage.setItem('currentUser', JSON.stringify({ token: token }));
  }

  /**
   * getToken
   */
  public getToken(): any {
    return JSON.parse(localStorage.getItem('currentUser'));
  }

  public userRole(): UserRole {
    return this.getToken().role;
  }
}

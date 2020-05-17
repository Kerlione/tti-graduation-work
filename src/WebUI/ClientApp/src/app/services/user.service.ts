import { Injectable } from '@angular/core';
import { UserRole } from '../models/user-role';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }

  /**
   * storeToken
token:string   */
  public storeToken(token: string) {
    const helper = new JwtHelperService();

    const decodedToken = helper.decodeToken(token);
    localStorage.setItem('currentUser', JSON.stringify({ token: decodedToken }));
    localStorage.setItem('pureToken', token);
  }

  /**
   * getToken
   */
  public getTokenData(): any {
    return JSON.parse(localStorage.getItem('currentUser'));
  }

  /**
   * getToken
   */
  public getToken(): string {
    return localStorage.getItem('pureToken');
  }

  public userRole(): UserRole {
    return this.getTokenData().role;
  }
}

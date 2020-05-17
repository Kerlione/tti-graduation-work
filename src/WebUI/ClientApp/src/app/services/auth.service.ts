import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  public checkAuthenticated(): boolean {
    const token = localStorage.getItem('pureToken');
    if (!token) {
      return false;
    }
    const helper = new JwtHelperService();

    const isExpired = helper.isTokenExpired(token);
    return !isExpired;
  }
}

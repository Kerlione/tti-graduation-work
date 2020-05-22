import { Injectable, Output } from '@angular/core';
import { UserRole, Supervisor, Administrator, Student } from '../models/user-role';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './auth.service';
import { Section } from '../common/sidenav/sidenav.component';
import { EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private authService: AuthService) { }

  authorizationEvent: EventEmitter<string> = new EventEmitter();

  public storeToken(token: string) {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(token);
    localStorage.setItem('currentUser', JSON.stringify({ token: decodedToken }));
    localStorage.setItem('pureToken', token);
  }

  public getTokenData(): any {
    return JSON.parse(localStorage.getItem('currentUser')).token;
  }

  public getToken(): string {
    return localStorage.getItem('pureToken');
  }

  public userRole(): string {
    return this.getTokenData().role;
  }

  public clearData(): boolean {
    localStorage.removeItem('currentUser');
    localStorage.removeItem('pureToken');
    return true;
  }

  public getGivenName(): string {
    return this.getTokenData().given_name;
  }

  public roleAssigned(role: UserRole): boolean {
    if (!this.authService.checkAuthenticated()) {
      return false;
    }
    return this.userRole() === role.name;
  }

  public getUserId(): number {
    return this.getTokenData().nameid;
  }


  public getMenuSections(): Section[] {
    console.log('Updated menu');
    return [
      {
        name: 'Home',
        path: '/home',
        allowed: true
      },
      {
        name: 'Graduation Papers',
        path: '/view/graduation-papers',
        allowed: this.roleAssigned(Supervisor) || this.roleAssigned(Administrator),
      },
      {
        name: 'Students',
        path: '/view/students',
        allowed: this.roleAssigned(Supervisor)
      },
      {
        name: 'Graduation Paper',
        path: '/graduation-paper/start',
        allowed: this.roleAssigned(Student)
      },
      {
        name: 'Supervisors',
        path: '/view/supervisors',
        allowed: this.authService.checkAuthenticated()
      },
      {
        name: 'Users',
        path: '/view/users',
        allowed: this.roleAssigned(Administrator)
      }
    ];
  }
}

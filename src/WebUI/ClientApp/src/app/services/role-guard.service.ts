import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from './user.service';
import { AuthService } from './auth.service';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuardService implements CanActivate {

  constructor(public userService: UserService,
    public authService: AuthService,
    public router: Router,
    public notificationService: NotificationService) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data.expectedRole;

    if (!this.authService.checkAuthenticated()) {
      this.router.navigate(['login']);
      this.notificationService.error('Unauthorized access!');
      return false;
    }
    if (!this.userService.roleAssigned(expectedRole)) {
      this.router.navigate(['home']);
      this.notificationService.warn('No access!');
      return false;
    }
    return true;
  }
}

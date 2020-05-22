import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';
import { UserRole, Administrator, Student, Supervisor } from 'src/app/models/user-role';

export interface Section {
  name: string;
  path: string;
  allowed: boolean;
}

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent implements OnInit {

  links: Section[] = this.userService.getMenuSections();
  subscription: any;
  constructor(public authService: AuthService,
    public userService: UserService,
    public router: Router) { }

  ngOnInit() {
    this.subscription = this.userService.authorizationEvent.subscribe(res => {
      this.links = this.userService.getMenuSections();
    });
  }

  public logout() {
    if (this.authService.checkAuthenticated()) {
      this.userService.clearData();
    }
    this.router.navigateByUrl('/login');
  }

  public getName(): string {
    return this.userService.getGivenName();
  }
}

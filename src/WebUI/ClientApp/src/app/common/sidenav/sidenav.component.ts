import { Component, OnInit } from '@angular/core';
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

  links: Section[] = [
    {
      name: 'Home',
      path: '/home',
      allowed: true
    },
    {
      name: 'Graduation Papers',
      path: '/view/graduation-papers',
      allowed: this.userService.roleAssigned(Supervisor) || this.userService.roleAssigned(Administrator),
    },
    {
      name: 'Students',
      path: '/view/students',
      allowed: this.userService.roleAssigned(Supervisor)
    },
    {
      name: 'Graduation Paper',
      path: '/graduation-paper/start',
      allowed: this.userService.roleAssigned(Student)
    },
    {
      name: 'Supervisors',
      path: '/view/supervisors',
      allowed: this.authService.checkAuthenticated()
    },
    {
      name: 'Users',
      path: '/view/users',
      allowed: this.userService.roleAssigned(Administrator)
    }
  ];

  constructor(public authService: AuthService,
    public userService: UserService,
    public router: Router) { }

  ngOnInit(): void {
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

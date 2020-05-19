import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

export interface Section {
  name: string;
  path: string;
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
      path: '/home'
    },
    {
      name: 'Graduation Papers',
      path: '/view/graduation-papers'
    },
    {
      name: 'Students',
      path: '/view/students'
    },
    {
      name: 'Graduation Paper',
      path: '/graduation-paper/start'
    },
    {
      name: 'Supervisors',
      path: '/view/supervisors'
    },
    {
      name: 'Users',
      path: '/view/users'
    }
  ];

  constructor(public authService: AuthService,
    public userService: UserService,
    public router: Router) { }

  ngOnInit(): void {
  }

  public logout() {
    console.log(this.userService);
    if (this.authService.checkAuthenticated()) {
      this.userService.clearData();
    }
    this.router.navigateByUrl('/login');
  }

  public getName(): string {
    return this.userService.getGivenName();
  }
}

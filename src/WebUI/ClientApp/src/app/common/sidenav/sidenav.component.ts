import { Component, OnInit } from '@angular/core';

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
    }
  ]

  constructor() { }

  ngOnInit(): void {
  }

}

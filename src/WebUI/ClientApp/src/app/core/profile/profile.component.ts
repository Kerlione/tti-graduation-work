import { Component, OnInit } from '@angular/core';
import { ProfileVm, HomeClient } from 'src/app/tti_graduation_work-api';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(private homeClient: HomeClient,
    private notificationService: NotificationService) { }
  profile: ProfileVm;
  ngOnInit(): void {
    this.homeClient.getProfile().subscribe(result => {
      this.profile = result;
    }, error => {
      this.notificationService.error(error);
    });
  }

}

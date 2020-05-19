import { Component, OnInit } from '@angular/core';
import { HomeClient, FeedItem } from 'src/app/tti_graduation_work-api';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  feeds: FeedItem[];
  isDataLoading: boolean = true;
  constructor(private homeClient: HomeClient,
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    this.refreshFeed();
  }

  private refreshFeed() {
    this.homeClient.getNews().subscribe(result => {
      if (result) {
        this.feeds = result.feedItems;
        this.isDataLoading = false;
      }
    },
      error => {
        this.notificationService.error(error);
        this.isDataLoading = false;
      });
  }
}

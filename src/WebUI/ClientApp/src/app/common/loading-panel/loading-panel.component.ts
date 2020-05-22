import { Component, OnInit, Inject, Input } from '@angular/core';

@Component({
  selector: 'app-loading-panel',
  templateUrl: './loading-panel.component.html',
  styleUrls: ['./loading-panel.component.css']
})
export class LoadingPanelComponent implements OnInit {

  @Input() isDataLoading: boolean;
  constructor(
  ) { }

  ngOnInit(): void {
  }

}

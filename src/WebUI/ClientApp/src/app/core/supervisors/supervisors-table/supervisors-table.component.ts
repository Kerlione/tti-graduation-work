import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { SupervisorsClient, GetSupervisorsQuery, SupervisorDto2 } from 'src/app/tti_graduation_work-api';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-supervisors-table',
  templateUrl: './supervisors-table.component.html',
  styleUrls: ['./supervisors-table.component.css'],
  providers: [NotificationService]
})
export class SupervisorsTableComponent implements AfterViewInit, OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<SupervisorDto2>;
  dataSource: SupervisorDto2[];
  tableDs;
  pageEvent: PageEvent;
  isDataLoading: boolean = true;
  totalCount = 0;
  defaultPageSize: number = 10;
  query: GetSupervisorsQuery;
  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['firstName', 'lastName', 'faculty', 'actions'];

  constructor(
    private supervisorsClient: SupervisorsClient,
    private notificationService: NotificationService) {

  }

  ngOnInit() {
    this.query = GetSupervisorsQuery.fromJS({take: +this.defaultPageSize, skip: +0});
    console.log(this.query);
    this.getData(this.query);
  }

  ngAfterViewInit() {
  }

  private getData(query: GetSupervisorsQuery) {
    this.isDataLoading = true;
    this.supervisorsClient.getSupervisors(query).subscribe(
      result => {
        if (result.supervisors.length) {
          this.dataSource = result.supervisors;
          this.totalCount = result.total;
          this.tableDs = new MatTableDataSource(this.dataSource);
          this.isDataLoading = false;
          this.tableDs.sort = this.sort;
          this.tableDs.paginator = this.paginator;
          this.tableDs.dataSource = this.dataSource;
        }
      },
      error => {
        console.error(error);
        this.isDataLoading = false;
      });
  }

  public getPagedData(event?: PageEvent) {
    this.query = GetSupervisorsQuery.fromJS(
      {
        take: this.paginator.pageSize,
        skip: this.paginator.pageSize * this.paginator.pageIndex
      }
    );
    this.isDataLoading = true;
    this.supervisorsClient.getSupervisors(this.query).subscribe(
      result => {
        if (result.supervisors.length) {
          this.dataSource = result.supervisors;
          this.totalCount = result.total;
          this.tableDs = new MatTableDataSource(this.dataSource);
          this.isDataLoading = false;
          this.tableDs.sort = this.sort;
          this.tableDs.dataSource = this.dataSource;
        }
      },
      error => {
        console.error(error);
        this.isDataLoading = false;
      });
    console.log(this.dataSource);
    return event;
  }
}

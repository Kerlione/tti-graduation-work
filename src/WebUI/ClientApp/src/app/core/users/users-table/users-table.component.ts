import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { UsersClient, UserDto, GetSupervisorsQuery, GetUsersQuery, RoleDto, StatusDto, LockUserCommand, UnlockUserCommand } from 'src/app/tti_graduation_work-api';
import { NotificationService } from 'src/app/services/notification.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.css']
})
export class UsersTableComponent implements OnInit, AfterViewInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<UserDto>;
  dataSource: UserDto[];
  roles: RoleDto[];
  usersStatuses: StatusDto[];
  tableDs;
  pageEvent: PageEvent;
  isDataLoading: boolean = true;
  totalCount = 0;
  defaultPageSize: number = 10;
  query: GetUsersQuery;

  displayedColumns = ['username', 'role', 'status', 'actions'];

  constructor(
    private usersClient: UsersClient,
    private notificationService: NotificationService) {

  }

  ngOnInit() {
    this.query = GetUsersQuery.fromJS({ take: +this.defaultPageSize, skip: +0 });
    this.getData(this.query);
  }

  ngAfterViewInit() {
  }

  private getData(query: GetUsersQuery) {
    this.isDataLoading = true;
    this.usersClient.getUsers(query).subscribe(
      result => {
        if (result.users.length) {
          this.dataSource = result.users;
          this.totalCount = result.total;
          this.usersStatuses = result.statuses;
          this.roles = result.roles;
          this.tableDs = new MatTableDataSource(this.dataSource);
          this.isDataLoading = false;
          this.tableDs.sort = this.sort;
          this.tableDs.paginator = this.paginator;
          this.tableDs.dataSource = this.dataSource;
        }
      },
      error => {
        this.notificationService.error(error);
        this.isDataLoading = false;
      });
  }

  public getPagedData(event?: PageEvent) {
    this.query = GetUsersQuery.fromJS(
      {
        take: this.paginator.pageSize,
        skip: this.paginator.pageSize * this.paginator.pageIndex
      }
    );
    this.getData(this.query);
    return event;
  }

  public findRole(id: number) {
    return this.roles.find(x => x.value === id).name;
  }
  public findStatus(id: number) {
    return this.usersStatuses.find(x => x.value === id).name;
  }

  public lock(id: number) {
    this.usersClient.lock(id, LockUserCommand.fromJS({ id: id })).subscribe(result => {
      this.notificationService.success('User locked!');
      this.getData(this.query);
    }, error => {
      this.notificationService.error(error);
    });
  }

  public unlock(id: number) {
    this.usersClient.unlock(id, UnlockUserCommand.fromJS({ id: id })).subscribe(result => {
      this.notificationService.success('User unlocked!');
      this.getData(this.query);
    }, error => {
      this.notificationService.error(error);
    });
  }

  public synchronize() {
    this.usersClient.sync().subscribe(result => {
      if (result) {
        // open dialog with data about sync results

        this.getData(this.query);
      }
    }, error => {
      this.notificationService.error(error);
    });
  }
}

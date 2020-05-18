import { AfterViewInit, Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';

import {
  GraduationPaperDto,
  GraduationPaperClient,
  GetGraduationPapersQuery,
  PaperTypeDto,
  PaperStatusDto
} from 'src/app/tti_graduation_work-api';
import { MatButton } from '@angular/material/button';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-graduation-papers-table',
  templateUrl: './graduation-papers-table.component.html',
  styleUrls: ['./graduation-papers-table.component.css'],
  providers: [NotificationService]
})
export class GraduationPapersTableComponent implements AfterViewInit, OnInit, OnDestroy {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<GraduationPaperDto>;
  @ViewChild(MatButton) button: MatButton;
  dataSource: GraduationPaperDto[];
  tableDs;
  isDataLoading = true;
  pageEvent: PageEvent;
  defaultPageSize = 10;
  totalCount = 0;
  paperTypes: PaperTypeDto[];
  paperStatuses: PaperStatusDto[];

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['title', 'supervisor', 'student', 'paperStatus', 'paperType', 'actions'];
  constructor(private graduationPapersClient: GraduationPaperClient,
    private notificationService: NotificationService) {
    this.graduationPapersClient = graduationPapersClient;
  }
  query: GetGraduationPapersQuery;
  ngOnInit() {
    this.query = GetGraduationPapersQuery.fromJS({ take: this.defaultPageSize, skip: 0 });
    this.getData(this.query);
  }

  private getData(query: GetGraduationPapersQuery) {
    this.isDataLoading = true;
    this.graduationPapersClient.get(this.query).subscribe(
      result => {
        if (result.graduationPapers.length) {
          this.dataSource = result.graduationPapers;
          this.totalCount = result.total;
          this.paperStatuses = result.paperStatuses;
          this.paperTypes = result.paperTypes;
          this.tableDs = new MatTableDataSource(this.dataSource);
          this.isDataLoading = false;
          this.tableDs.sort = this.sort;
          this.tableDs.paginator = this.paginator;
          this.tableDs.dataSource = this.dataSource;
        }
      },
      error => {
        this.notificationService.error(error);
        console.error(error);
        this.isDataLoading = false;
      });
  }

  /**
   * getPagedData
   */
  public getPagedData(event?: PageEvent) {
    this.query = GetGraduationPapersQuery.fromJS(
      {
        take: this.paginator.pageSize,
        skip: this.paginator.pageSize * this.paginator.pageIndex
      }
    );
    this.isDataLoading = true;
    this.graduationPapersClient.get(this.query).subscribe(
      result => {
        if (result.graduationPapers.length) {
          this.dataSource = result.graduationPapers;
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

  ngAfterViewInit() {
  }

  ngOnDestroy() {
    this.query = null;
  }

  public openDetails(id: number) {
  }
}

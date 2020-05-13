import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { StudentDto, StudentsClient } from 'src/app/tti_graduation_work-api';

@Component({
  selector: 'app-students-table',
  templateUrl: './students-table.component.html',
  styleUrls: ['./students-table.component.css']
})
export class StudentsTableComponent implements AfterViewInit, OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<StudentDto>;
  dataSource: StudentDto[];
  tableDs;
  isDataLoading = true;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['firstName', 'lastName', 'faculty', 'programe'];

  constructor(private studentsClient: StudentsClient) {
    this.studentsClient = studentsClient;
  }

  ngOnInit() {
    this.studentsClient.get().subscribe(
      result => {
        if (result.students.length) {
          this.dataSource = result.students;
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

  ngAfterViewInit() {
  }
}

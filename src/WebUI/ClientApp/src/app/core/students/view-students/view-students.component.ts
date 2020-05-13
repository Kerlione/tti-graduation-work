import {Component, ViewChild, AfterViewInit} from '@angular/core';
import { StudentsClient, StudentsVm, StudentDto } from '../../../tti_graduation_work-api';
import {PageEvent} from '@angular/material/paginator';

@Component({
  selector: 'app-view-students',
  templateUrl: './view-students.component.html',
  styleUrls: ['./view-students.component.css']
})
export class ViewStudentsComponent implements AfterViewInit {  
  displayedColumns: string[] = ['firstName', 'lastName', 'faculty', 'programe'];  
  isLoadingResults = true;

  // Paginator section
  length = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100]

  vm: StudentsVm;
  students: StudentDto[];

  constructor(private studentsClient: StudentsClient) { 
    this.studentsClient = studentsClient;
  }
  ngAfterViewInit(): void {
    this.studentsClient.get().subscribe(
      result => {
          this.vm = result;
          if(this.vm.students.length){
            this.students = this.vm.students;
            for (let i = 0; i < 50; i++) {
              this.students = this.students.concat(this.vm.students);              
            }
            this.length = this.students.length;
          }
          this.isLoadingResults = false;
      },
      error => {
        console.error(error);
        this.isLoadingResults = false;
      }
    );
  }
  pageEvent: PageEvent;
}
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { StepDto, StepStatusDto, StepTypeDto, StepsClient, GetStepQuery } from 'src/app/tti_graduation_work-api';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-graduation-papers-details',
  templateUrl: './graduation-papers-details.component.html',
  styleUrls: ['./graduation-papers-details.component.css']
})
export class GraduationPapersDetailsComponent implements AfterViewInit, OnInit {
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<StepDto>;
  dataSource: StepDto[];
  stepStatuses: StepStatusDto[];
  stepTypes: StepTypeDto[];
  paperId: number;
  tableDs;
  isDataLoading: boolean = true;

  displayedColumns = ['stepType', 'stepStatus'];

  constructor(private stepsClient: StepsClient,
    private route: ActivatedRoute) {

  }


  ngOnInit() {
    this.route.params.subscribe(params => {
      this.paperId = params['id'];

      console.log(`${this.paperId}`);
      this.stepsClient.get(this.paperId).subscribe(
        result => {
          if (result.steps.length) {
            this.dataSource = result.steps;
            this.stepStatuses = result.statuses;
            this.stepTypes = result.types;
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
    });

  }

  ngAfterViewInit() {    
  }
}

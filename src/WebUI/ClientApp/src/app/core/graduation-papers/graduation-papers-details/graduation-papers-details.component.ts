import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { StepDto, StepStatusDto, StepTypeDto, StepsClient, GetStepQuery, ApproveStepCommand } from 'src/app/tti_graduation_work-api';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

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

  displayedColumns = ['stepType', 'stepStatus', 'actions'];

  constructor(private stepsClient: StepsClient,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar) {

  }


  ngOnInit() {
    this.route.params.subscribe(params => {
      this.paperId = params['id'];

      console.log(`${this.paperId}`);
      this.stepsClient.getSteps(this.paperId).subscribe(
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

  public notify(id: number) {

  }
  public edit(id: number) {

  }
  public approve(id: number) {
    let stepId: number = this.dataSource[id].id;
    this.stepsClient.approveStep(this.paperId, stepId,
      ApproveStepCommand.fromJS({
        supervisorId: 1,
        graduationPaperId: this.paperId,
        stepId: stepId
      })).subscribe(
        result => {
          console.log(result);
        },
        error => {
          this.snackBar.open(error);
          console.log(error);
        }
      );
  }
  public reject(id: number) {

  }

  public getTypeByValue(value: number): string {
    return this.stepTypes.find(x => x.value === value).name;
  }

  public getStatusByValue(value: number): string {
    return this.stepStatuses.find(x => x.value === value).name;
  }

  public allowEdit(id: number) {
    let row: StepDto = this.dataSource[id];
    if (this.dataSource[id - 1]) {
      if (this.dataSource[id - 1].stepStatus === 5) {
        if (row.stepStatus === 0 || row.stepStatus === 1) {
          return true;
        } else {
          return false;
        }
      }
    } else {
      if (row.stepStatus === 0 || row.stepStatus === 1) {
        return true;
      } else {
        return false;
      }
    }
  }
}

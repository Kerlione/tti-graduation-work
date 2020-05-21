import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import {
  StepDto, StepStatusDto, StepTypeDto, StepsClient, ApproveStepCommand,
  NotifySupervisorCommand, NotifyStudentCommand, RejectStepCommand, GraduationPaperDto, GraduationPaperDto3, GraduationPaperDto2
} from 'src/app/tti_graduation_work-api';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from 'src/app/services/user.service';
import { UserRole, Supervisor, Student } from 'src/app/models/user-role';
import { NotificationService } from 'src/app/services/notification.service';
import { MatDialog } from '@angular/material/dialog';
import { RejectDialogComponent } from './reject-dialog/reject-dialog.component';
import { StringSplitService } from 'src/app/services/string-split.service';

@Component({
  selector: 'app-graduation-papers-details',
  templateUrl: './graduation-papers-details.component.html',
  styleUrls: ['./graduation-papers-details.component.css'],
  providers: [UserService, NotificationService, StringSplitService]
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
  graduationPaper: GraduationPaperDto3;

  displayedColumns = ['stepType', 'stepStatus', 'actions'];

  colors: string[] = [
    'gray', 'blue', 'orange', 'purple', 'red', 'green'
  ];

  constructor(private stepsClient: StepsClient,
    private route: ActivatedRoute,
    private userService: UserService,
    private notificationService: NotificationService,
    private dialog: MatDialog,
    private stringSplitService: StringSplitService) {

  }


  ngOnInit() {
    this.route.params.subscribe(params => {
      this.paperId = +params['id'];
      this.getSteps();

    });

  }
  private getSteps() {
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
          this.graduationPaper = result.graduationPaper;
        }
      },
      error => {
        this.notificationService.error(error);
        this.isDataLoading = false;
      });
  }
  ngAfterViewInit() {
  }

  public allowNotify(row: StepDto): boolean {
    if (this.userService.roleAssigned(Student)) {
      return row.stepStatus === 2;
    } else {
      if (this.userService.roleAssigned(Supervisor)) {
        return row.stepStatus < 2 || row.stepStatus !== 5;
      }
    }
  }

  public notify(id: number) {
    let stepId: number = this.dataSource[id].id;
    if (this.userService.roleAssigned(Student)) {
      this.stepsClient.notifySupervisor(this.paperId,
        NotifySupervisorCommand.fromJS({ graduationPaperId: this.paperId, stepId: stepId })).subscribe(result => {
          this.notificationService.success('Supervisor notified');
        },
          error => {
            this.notificationService.error(error);
          });
    } else {
      if (this.userService.roleAssigned(Supervisor)) {
        this.stepsClient.notifyStudent(this.paperId,
          NotifyStudentCommand.fromJS({ graduationPaperId: this.paperId, stepId: stepId })).subscribe(result => {
            this.notificationService.success('Student notified');
          },
            error => {
              this.notificationService.error(error);
            });
      }
    }

  }
  public edit(id: number) {

  }
  public approve(id: number) {
    let stepId: number = this.dataSource[id].id;
    this.stepsClient.approveStep(this.paperId, stepId,
      ApproveStepCommand.fromJS({
        supervisorId: this.graduationPaper.supervisorId,
        graduationPaperId: this.paperId,
        stepId: stepId
      })).subscribe(
        result => {
          this.notificationService.success('Approved');
          this.getSteps();
        },
        error => {
          this.notificationService.error(error);
        }
      );
  }
  public reject(id: number) {
    let stepId: number = this.dataSource[id].id;
    let comment: string = '';
    const dialogRef = this.dialog.open(RejectDialogComponent, {
      width: '300px',
      data: { comment: comment }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      if (result === 1) {
        this.stepsClient.rejectStep(this.paperId, stepId,
          RejectStepCommand.fromJS({ stepId: stepId, graduationPaperId: this.paperId, reason: result }))
          .subscribe(res => {
            this.notificationService.success('Rejected');
            this.getSteps();
          },
            error => {
              this.notificationService.error(error);
            });
      }
    });
  }

  public getTypeByValue(value: number): string {
    return this.stringSplitService.split(this.stepTypes.find(x => x.value === value).name);
  }

  public getStatusByValue(value: number): string {
    return this.stringSplitService.split(this.stepStatuses.find(x => x.value === value).name);
  }

  public allowEdit(id: number) {
    let row: StepDto = this.dataSource[id];
    if (this.dataSource[id - 1]) {
      if (this.dataSource[id - 1].stepStatus === 5) {
        if (row.stepStatus !== 2) {
          return true;
        } else {
          return false;
        }
      }
    } else {
      if (row.stepStatus !== 2) {
        return true;
      } else {
        return false;
      }
    }
  }

  public stepProgress(): number {
    const percentage = 100 / this.dataSource.length * this.dataSource.filter(x => x.stepStatus === 5).length;
    return percentage;
  }

}

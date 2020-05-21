import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { SupervisorsClient, GetSupervisorQuery, CreateTopicCommand, DeleteTopicCommand, UpdateTopicCommand, CreateFieldCommand, DeleteFieldCommand, UpdateFieldCommand, FieldOfInterestDto, ThesisTopicDto, SupervisorDto, UpdateStudentLimitCommand } from 'src/app/tti_graduation_work-api';
import { ActivatedRoute } from '@angular/router';
import { NotificationService } from 'src/app/services/notification.service';
import { UserService } from 'src/app/services/user.service';
import { AddDialogComponent } from './dialogs/add-dialog/add-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { EditDialogComponent } from './dialogs/edit-dialog/edit-dialog.component';
import { UpdateStudentLimitComponent } from './dialogs/update-student-limit/update-student-limit.component';
import { Administrator } from 'src/app/models/user-role';

@Component({
  selector: 'app-supervisor-details',
  templateUrl: './supervisor-details.component.html',
  styleUrls: ['./supervisor-details.component.css'],
  providers: [NotificationService, UserService]
})
export class SupervisorDetailsComponent implements AfterViewInit, OnInit {
  @ViewChild('interestsPaginator', { read: MatPaginator }) interestsPaginator: MatPaginator;
  @ViewChild('topicsPatinator', { read: MatPaginator }) topicsPaginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) interestsTable: MatTable<FieldOfInterestDto>;
  @ViewChild(MatTable) topicsTable: MatTable<ThesisTopicDto>;
  topics: ThesisTopicDto[];
  topicsDs;
  interests: FieldOfInterestDto[];
  interestsDs;
  supervisorId: number;
  supervisor: SupervisorDto;
  isDataLoading = true;
  administratorRole = Administrator;
  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['titleEn', 'titleRu', 'titleLv', 'actions'];

  constructor(private supervisorClient: SupervisorsClient,
    private route: ActivatedRoute,
    private notificationService: NotificationService,
    public userService: UserService,
    public dialog: MatDialog) {

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.supervisorId = +params['id'];

      this.supervisorClient.getProfile(this.supervisorId, GetSupervisorQuery.fromJS({ id: this.supervisorId })).subscribe(result => {
        if (result) {
          this.topics = result.topics;
          this.topicsDs = new MatTableDataSource(this.topics);
          this.topicsDs.paginator = this.topicsPaginator;
          this.interests = result.fieldsOfInterest;
          this.interestsDs = new MatTableDataSource(this.interests);
          this.interestsDs.paginator = this.interestsPaginator;
          this.supervisor = result;
          this.isDataLoading = false;
        }
      }, error => {
        this.notificationService.error(error);
        this.isDataLoading = false;
        console.log(error);
      });
    });
  }

  ngAfterViewInit() {
  }

  private refreshTables() {
    this.supervisorClient.getProfile(this.supervisorId, GetSupervisorQuery.fromJS({ id: this.supervisorId })).subscribe(result => {
      if (result) {
        this.topics = result.topics;
        this.topicsDs = new MatTableDataSource(this.topics);
        this.interests = result.fieldsOfInterest;
        this.interestsDs = new MatTableDataSource(this.interests);
        this.supervisor = result;
      }
    }, error => {
      this.notificationService.error(error);
    });
  }
  /**
   * addTopic
   */
  public addTopic() {
    let topic = new ThesisTopicDto();
    let dialog = this.dialog.open(AddDialogComponent, {
      data: { fields: topic, type: 'topic' },
      width: '600px',
    });

    dialog.afterClosed().subscribe(result => {
      if (result === 1) {
        let command = CreateTopicCommand.fromJS({
          supervisorId: this.supervisorId,
          title_EN: topic.title_EN, title_LV: topic.title_LV, title_RU: topic.title_RU
        });
        this.supervisorClient.addTopic(this.supervisorId, command).subscribe(result => {
          this.notificationService.success('Topic added');
          this.refreshTables();
        }, error => {
          this.notificationService.error(error);
        });
      }
    })

  }

  public deleteTopic(id: number) {
    let command = DeleteTopicCommand.fromJS({ supervisorId: this.supervisorId, topicId: id });
    this.supervisorClient.deleteTopic(this.supervisorId, id, command).subscribe(result => {
      this.notificationService.success('Topic removed');
      this.refreshTables();
    }, error => {
      this.notificationService.error(error);
    });
  }
  public editTopic(row: ThesisTopicDto) {
    let dialog = this.dialog.open(EditDialogComponent, {
      data: { fields: row, type: 'topic' },
      width: '600px',
    });
    dialog.afterClosed().subscribe(result => {
      if (result === 1) {
        let command = UpdateTopicCommand.fromJS({
          supervisorId: this.supervisorId,
          title_EN: row.title_EN, title_LV: row.title_LV, title_RU: row.title_RU,
          topicId: row.id
        });
        this.supervisorClient.updateTopic(this.supervisorId, row.id, command).subscribe(res => {
          this.notificationService.success('Topic updated');
          this.refreshTables();
        }, error => {
          this.notificationService.error(error);
          this.refreshTables();
        });
      }
    });
  }

  public addField() {
    let field = new FieldOfInterestDto();
    let dialog = this.dialog.open(AddDialogComponent, {
      data: { fields: field, type: 'field' },
      width: '600px',
    });
    dialog.afterClosed().subscribe(result => {
      if (result === 1) {
        let command = CreateFieldCommand.fromJS({
          supervisorId: this.supervisorId,
          title_EN: field.title_EN, title_LV: field.title_LV, title_RU: field.title_RU
        });
        this.supervisorClient.addField(this.supervisorId, command).subscribe(res => {
          this.notificationService.success('Field added');
          this.refreshTables();
        }, error => {
          this.notificationService.error(error);
        });
      }
    });
  }

  public deleteField(id: number) {
    let command = DeleteFieldCommand.fromJS({
      supervisorId: this.supervisorId,
      fieldId: id
    });
    this.supervisorClient.deleteField(this.supervisorId, id, command).subscribe(result => {
      this.notificationService.success('Field removed');
      this.refreshTables();
    }, error => {
      this.notificationService.error(error);
    });
  }
  public editField(row: FieldOfInterestDto) {
    let dialog = this.dialog.open(EditDialogComponent, {
      data: { fields: row, type: 'field' },
      width: '600px',
    });
    dialog.afterClosed().subscribe(result => {
      if (result === 1) {
        let command = UpdateFieldCommand.fromJS({
          supervisorId: this.supervisorId,
          title_EN: row.title_EN, title_LV: row.title_LV, title_RU: row.title_RU,
          fieldId: row.id
        });

        this.supervisorClient.updateField(this.supervisorId, row.id, command).subscribe(res => {
          this.notificationService.success('Field updated');
          this.refreshTables();
        }, error => {
          this.notificationService.error(error);
          this.refreshTables();
        });
      }
    });
  }

  public updateStudentLimit() {
    let data = { studentLimit: this.supervisor.studentLimit };
    let dialog = this.dialog.open(UpdateStudentLimitComponent, {
      data: data
    });
    dialog.afterClosed().subscribe(result => {
      if (result === 1) {
        let command = UpdateStudentLimitCommand.fromJS({ supervisorId: this.supervisorId, value: data.studentLimit });
        this.supervisorClient.updateStudentLimit(this.supervisorId, command).subscribe(res => {
          if (res) {
            this.notificationService.success('Student limit updated!');
            this.refreshTables();
          }
        }, error => {
          this.notificationService.error(error);
        });
      }
    });
  }

  public allowEdit(): boolean {
    let entityId = this.userService.getUserId();
    return entityId == this.supervisorId;
  }
}


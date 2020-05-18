import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { SupervisorsClient, GetSupervisorQuery, CreateTopicCommand, DeleteTopicCommand, UpdateTopicCommand, CreateFieldCommand, DeleteFieldCommand, UpdateFieldCommand, FieldOfInterestDto, ThesisTopicDto, SupervisorDto } from 'src/app/tti_graduation_work-api';
import { ActivatedRoute } from '@angular/router';
import { NotificationService } from 'src/app/services/notification.service';
import { UserService } from 'src/app/services/user.service';
import { AddDialogComponent } from './dialogs/add-dialog/add-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-supervisor-details',
  templateUrl: './supervisor-details.component.html',
  styleUrls: ['./supervisor-details.component.css'],
  providers: [NotificationService, UserService]
})
export class SupervisorDetailsComponent implements AfterViewInit, OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
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
  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['titleEn', 'titleRu', 'titleLv', 'actions'];

  constructor(private supervisorClient: SupervisorsClient,
    private route: ActivatedRoute,
    private notificationService: NotificationService,
    private userService: UserService,
    public dialog: MatDialog) {

  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.supervisorId = +params['id'];

      this.supervisorClient.getProfile(this.supervisorId, GetSupervisorQuery.fromJS({ id: this.supervisorId })).subscribe(result => {
        if (result) {
          this.topics = result.topics;
          this.topicsDs = new MatTableDataSource(this.topics);
          this.interests = result.fieldsOfInterest;
          this.interestsDs = new MatTableDataSource(this.interests);
          this.supervisor = result;
          console.log(this.supervisor);
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
      data: topic
    });

    dialog.afterClosed().subscribe(result => {
      if (result === 1) {
        let command = CreateTopicCommand.fromJS({
          supervisorId: this.supervisorId,
          titleEn: topic.title_EN, titleLv: topic.title_LV, titleRu: topic.title_RU
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
    }, error => {
      this.notificationService.error(error);
    });
  }
  public editTopic(id: number) {
    // open dialog for update
    let command = UpdateTopicCommand.fromJS({});
    this.supervisorClient.updateTopic(this.supervisorId, id, command).subscribe(result => {
      this.notificationService.success('Topic updated');
    }, error => {
      this.notificationService.error(error);
    });
  }

  public addField() {
    // open dialog and fill info
    let command = CreateFieldCommand.fromJS({});
    this.supervisorClient.addField(this.supervisorId, command).subscribe(result => {
      this.notificationService.success('Field added');
    }, error => {
      this.notificationService.error(error);
    });
  }

  public deleteField(id: number) {
    let topicId = this.interests[id].id;
    let command = DeleteFieldCommand.fromJS({});
    this.supervisorClient.deleteField(this.supervisorId, topicId, command).subscribe(result => {
      this.notificationService.success('Field removed');
    }, error => {
      this.notificationService.error(error);
    });
  }
  public editField(id: number) {
    // open dialog for update
    let command = UpdateFieldCommand.fromJS({});
    let topicId = this.interests[id].id;
    this.supervisorClient.updateField(this.supervisorId, topicId, command).subscribe(result => {
      this.notificationService.success('Field updated');
    }, error => {
      this.notificationService.error(error);
    });
  }
}


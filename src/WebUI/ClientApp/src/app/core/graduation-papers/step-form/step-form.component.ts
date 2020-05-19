import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { StepFormService } from 'src/app/services/step-form.service';
import { QuestionBase } from 'src/app/models/question-base';
import { ActivatedRoute, Router } from '@angular/router';
import {
  StepsClient, StepDto2, UpdateStepCommand, SendStepToReviewCommand,
  FinishStepCommand, GraduationPaperClient, FileParameter
} from 'src/app/tti_graduation_work-api';
import { FormGeneratorService } from 'src/app/services/form-generator.service';
import { NotificationService } from 'src/app/services/notification.service';
import { StepData } from 'src/app/models/step-data';

@Component({
  selector: 'app-step-form',
  templateUrl: './step-form.component.html',
  styleUrls: ['./step-form.component.css'],
  providers: [StepFormService, FormGeneratorService, NotificationService]
})
export class StepFormComponent implements OnInit {
  @Input() questions: QuestionBase<any>[];
  @Input() isDialog: boolean;
  @Output() close = new EventEmitter<any>();
  @Output() submit = new EventEmitter<any>();
  form: FormGroup = new FormGroup({});
  payLoad = '';
  stepId: number;
  paperId: number;
  stepData: StepDto2;
  stepDataModel: StepData;
  noPaper: boolean = false;
  fileToUpload: File = null;
  fileUrl;
  dataLoaded: Promise<boolean>;
  constructor(
    private sfs: StepFormService,
    private route: ActivatedRoute,
    private stepsClient: StepsClient,
    private fgs: FormGeneratorService,
    private notificationService: NotificationService,
    private graduationPaperClient: GraduationPaperClient,
    private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.paperId = +params['id'];
      this.stepId = +params['stepId'];
      if (this.paperId && this.stepId) {
        this.getStep();
      } else {
        this.noPaper = true;
        this.stepDataModel = this.fgs.generateForm(0);
      }
    });
  }

  onSubmit() {
    this.save();
  }

  private getStep() {
    this.stepsClient.getStep(this.paperId, this.stepId)
      .subscribe(result => {
        if (result) {
          this.stepData = result;
          console.log(this.stepData);
          this.stepsClient.getAvailableSupervisors().subscribe(res => {
            if (res.list.length) {
              this.stepDataModel = this.fgs.generateForm(this.stepData.stepType, res.list,
                this.stepData.stepStatus === 2 || this.stepData.stepStatus === 3 || this.stepData.stepStatus === 5);
              if (this.stepDataModel.isForm) {
                this.form = this.sfs.toFormGroup(this.stepDataModel.formData);
                console.log(this.stepData.data);
                this.form.setValue(JSON.parse(this.stepData.data));
              }
              if (this.stepData.stepStatus === 2 || this.stepData.stepStatus === 3 || this.stepData.stepStatus === 5) {
                this.form.disable();
              }
              //this.dataLoaded = Promise.resolve(true);
            }
          });
        }
      },
        error => {
          console.error(error);
          this.notificationService.error(error);
        });
  }

  public save() {
    this.stepsClient.updateStep(this.stepId,
      UpdateStepCommand.fromJS({ graduationPaperId: this.paperId, stepId: this.stepId, data: JSON.stringify(this.form.value) }))
      .subscribe(result => {
        this.notificationService.success('Saved!');
        this.getStep();
      },
        error => {
          this.notificationService.error(error);
        });
  }

  /**
   * sendToReview
   */
  public sendToReview() {
    this.stepsClient.sendToReview(this.paperId, this.stepId,
      SendStepToReviewCommand.fromJS({ stepId: this.stepId, graduationPaperId: this.paperId })).subscribe(result => {
        if (result) {
          this.notificationService.success('Sent to review');
          this.getStep();
        }
      }, error => {
        this.notificationService.error(error);
      });
  }

  /**
   * finishStep
   */
  public finishStep() {
    this.stepsClient.finishStep(this.paperId, this.stepId,
      FinishStepCommand.fromJS({ stepId: this.stepId, graduationPaperId: this.paperId })).subscribe(result => {
        this.notificationService.success('Finished successfully');
        this.getStep();
      }, error => {
        this.notificationService.error(error);
      });
  }

  onCancel() {
    this.close.emit(null);
  }

  public createPaper() {
    this.graduationPaperClient.getPaper().subscribe(result => {
      if (result) {
        this.router.navigateByUrl('view/graduation-papers/' + result.id + '/details');
      }
    },
      error => {
        this.notificationService.error(error);
      });
  }
  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    console.log(this.fileToUpload);
    const reader = new FileReader();
    reader.readAsDataURL(this.fileToUpload);

    reader.onload = (event: any) => {
      this.fileUrl = event.target.result;
    };

    let fileParam: FileParameter = {data: this.fileToUpload, fileName: this.fileToUpload.name};
    this.stepsClient.uploadAttachment(this.paperId, this.stepId, fileParam).subscribe(result => {

    });

    return;
  }
}

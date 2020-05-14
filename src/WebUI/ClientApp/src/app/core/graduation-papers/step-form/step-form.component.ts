import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { StepFormService } from 'src/app/services/step-form.service';
import { QuestionBase } from 'src/app/models/question-base';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { StepsClient, GetStepQuery, StepDto2 } from 'src/app/tti_graduation_work-api';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-step-form',
  templateUrl: './step-form.component.html',
  styleUrls: ['./step-form.component.css'],
  providers: [StepFormService]
})
export class StepFormComponent implements OnInit {
  @Input() questions: QuestionBase<any>[] = [];
  @Input() isDialog: boolean;

  @Output() close = new EventEmitter<any>();
  @Output() submit = new EventEmitter<any>();
  form: FormGroup;
  payLoad = '';
  stepId: number;
  paperId: number;
  stepData: StepDto2;
  constructor(
    private sfs: StepFormService,
    private _snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private stepsClient: StepsClient) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.paperId = params['id'];
      this.stepId = params['stepId'];
      let request = new GetStepQuery();
      request.graduationPaperId =  this.paperId;
      request.stepId = this.stepId;
      this.stepsClient.getStep(this.paperId, this.stepId,
        request)
        .subscribe(result => {
          if (result) {
            this.stepData = result;
            console.log(this.stepData);
            this.form = this.sfs.toFormGroup(this.questions);
          }
        },
          error => {
            console.error(error);
          });
    });
  }

  onSubmit() {
    this._snackBar.open(`Saved Successfully`, '', {
      duration: 2000,
      panelClass: 'snackbar-success',
      verticalPosition: 'top',
      horizontalPosition: 'right'
    }).afterOpened().subscribe(() => {
      this.submit.emit(this.form.value);
      this.payLoad = JSON.stringify(this.form.value);
      console.log('Saved the following values', this.payLoad);
    });
  }

  onCancel() {
    this.close.emit(null);
  }
}

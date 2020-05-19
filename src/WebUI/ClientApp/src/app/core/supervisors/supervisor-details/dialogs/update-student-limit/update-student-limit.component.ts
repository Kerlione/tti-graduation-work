import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-student-limit',
  templateUrl: './update-student-limit.component.html',
  styleUrls: ['./update-student-limit.component.css']
})
export class UpdateStudentLimitComponent {

  constructor(public dialogRef: MatDialogRef<UpdateStudentLimitComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

    studentLimitControl = new FormControl(this.data.studentLimit, Validators.min(0));

  onNoClick(): void {
    this.dialogRef.close();
  }

  submit() {
    // emppty stuff
    }

  public confirmUpdate(): any {
    console.log(this.data);
    return this.data;
  }

}

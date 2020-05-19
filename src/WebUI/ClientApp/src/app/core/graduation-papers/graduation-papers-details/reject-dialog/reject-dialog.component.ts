import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogData } from 'src/app/models/dialog-data';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-reject-dialog',
  templateUrl: './reject-dialog.component.html',
  styleUrls: ['./reject-dialog.component.css']
})
export class RejectDialogComponent {

  constructor(public dialogRef: MatDialogRef<RejectDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  formControl = new FormControl('', [
    Validators.required
  ]);

  onNoClick(): void {
    this.dialogRef.close(-1);
  }

  public getErrorMessage(): string {
    return 'Field is required';
  }

  public submit() {
    return this.data;
  }
}

import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private snackBar: MatSnackBar) { }

  public error(message: any) {
    this.snackBar.open(JSON.parse(message.response).detail, `Close`, {
      duration: 5000,
      panelClass: 'snackbar-error',
      verticalPosition: 'top',
      horizontalPosition: 'right'
    });
  }

  public success(message: string) {
    this.snackBar.open(message, `Close`, {
      duration: 2000,
      panelClass: 'snackbar-success',
      verticalPosition: 'top',
      horizontalPosition: 'right'
    });
  }

  public warn(message: string) {
    this.snackBar.open(message, `Close`, {
      duration: 2000,
      panelClass: 'snackbar-warn',
      verticalPosition: 'top',
      horizontalPosition: 'right'
    });
  }
}

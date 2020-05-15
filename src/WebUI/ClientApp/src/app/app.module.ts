import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { MatTableModule } from '@angular/material/table/';
import { MatSidenavModule } from '@angular/material/sidenav/';
import { MatListModule } from '@angular/material/list/';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDividerModule } from '@angular/material/divider';
import { MatSnackBarModule, MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';

import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';

import { AppComponent } from './app.component';
import { SidenavComponent } from '../app/common/sidenav/sidenav.component';
import { HomeComponent } from './core/home/home.component';
import { StudentsTableComponent } from './core/students/students-table/students-table.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { GraduationPapersTableComponent } from './core/graduation-papers/graduation-papers-table/graduation-papers-table.component';
import { MatButtonModule } from '@angular/material/button';
import { GraduationPapersDetailsComponent } from './core/graduation-papers/graduation-papers-details/graduation-papers-details.component';
import { StepFormComponent } from './core/graduation-papers/step-form/step-form.component';
import { FormFieldComponent } from './core/graduation-papers/step-form/form-field/form-field.component';
import { RejectDialogComponent } from './core/graduation-papers/graduation-papers-details/reject-dialog/reject-dialog.component';


const appRoutes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/home' },
  { path: 'home', component: HomeComponent },
  { path: 'view/graduation-papers', component: GraduationPapersTableComponent },
  { path: 'view/graduation-papers/:id/details', component: GraduationPapersDetailsComponent },
  { path: 'view/graduation-papers/:id/step/:stepId', component: StepFormComponent },
  { path: 'graduation-paper/start', component: StepFormComponent },
  { path: 'view/students', component: StudentsTableComponent }
]

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SidenavComponent,
    StudentsTableComponent,
    GraduationPapersTableComponent,
    GraduationPapersDetailsComponent,
    StepFormComponent,
    FormFieldComponent,
    RejectDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    RouterModule.forRoot(
      appRoutes
    ),
    ModalModule.forRoot(),
    NoopAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    // Material
    MatTableModule,
    MatSidenavModule,
    MatListModule,
    MatPaginatorModule,
    MatToolbarModule,
    MatProgressSpinnerModule,
    MatDividerModule,
    MatButtonModule,
    MatSnackBarModule,
    MatInputModule,
    MatFormFieldModule,
    MatTabsModule,
    MatSelectModule,
    MatRadioModule,
    MatDialogModule
  ],
  bootstrap: [AppComponent],
  providers: [
    { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 2500 } }
  ]
})
export class AppModule { }

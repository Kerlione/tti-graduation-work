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
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';

import { AppComponent } from './app.component';
import { SidenavComponent } from '../app/common/sidenav/sidenav.component';
import { HomeComponent } from './core/home/home.component';
import { StudentsTableComponent } from './core/students/students-table/students-table.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NoopAnimationsModule, BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GraduationPapersTableComponent } from './core/graduation-papers/graduation-papers-table/graduation-papers-table.component';
import { MatButtonModule } from '@angular/material/button';
import { GraduationPapersDetailsComponent } from './core/graduation-papers/graduation-papers-details/graduation-papers-details.component';
import { StepFormComponent } from './core/graduation-papers/step-form/step-form.component';
import { FormFieldComponent } from './core/graduation-papers/step-form/form-field/form-field.component';
import { RejectDialogComponent } from './core/graduation-papers/graduation-papers-details/reject-dialog/reject-dialog.component';
import { LoginComponent } from './common/login/login.component';
import { SupervisorsTableComponent } from './core/supervisors/supervisors-table/supervisors-table.component';
import { SupervisorDetailsComponent } from './core/supervisors/supervisor-details/supervisor-details.component';
import { AddDialogComponent } from './core/supervisors/supervisor-details/dialogs/add-dialog/add-dialog.component';
import { EditDialogComponent } from './core/supervisors/supervisor-details/dialogs/edit-dialog/edit-dialog.component';
import { DeleteDialogComponent } from './core/supervisors/supervisor-details/dialogs/delete-dialog/delete-dialog.component';
import { UpdateStudentLimitComponent } from './core/supervisors/supervisor-details/dialogs/update-student-limit/update-student-limit.component';
import { AuthorizeInterceptor } from './services/authorize.interceptor';
import { UsersTableComponent } from './core/users/users-table/users-table.component';
import { ProfileComponent } from './core/profile/profile.component';
import { RoleGuardService } from './services/role-guard.service';
import { AuthGuardService } from './services/auth-guard.service';
import { Administrator, Supervisor, Student } from './models/user-role';
import { LoadingPanelComponent } from './common/loading-panel/loading-panel.component';


const appRoutes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/login' },
  { path: 'login', pathMatch: 'full', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuardService] },
  { path: 'view/graduation-papers', component: GraduationPapersTableComponent, canActivate: [AuthGuardService] },
  { path: 'view/graduation-papers/:id/details', component: GraduationPapersDetailsComponent, canActivate: [AuthGuardService] },
  { path: 'view/graduation-papers/:id/step/:stepId', component: StepFormComponent, canActivate: [AuthGuardService] },
  {
    path: 'graduation-paper/start', component: StepFormComponent, canActivate: [RoleGuardService], data: {
      expectedRole: Student
    }
  },
  {
    path: 'view/students', component: StudentsTableComponent, canActivate: [RoleGuardService],
    data: {
      expectedRole: Supervisor
    }
  },
  { path: 'view/supervisors', component: SupervisorsTableComponent, canActivate: [AuthGuardService] },
  { path: 'view/supervisors/:id/details', component: SupervisorDetailsComponent },
  {
    path: 'view/users', component: UsersTableComponent, canActivate: [RoleGuardService], data: {
      expectedRole: Administrator
    }
  },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuardService] }
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
    RejectDialogComponent,
    LoginComponent,
    SupervisorsTableComponent,
    SupervisorDetailsComponent,
    AddDialogComponent,
    EditDialogComponent,
    DeleteDialogComponent,
    UpdateStudentLimitComponent,
    UsersTableComponent,
    ProfileComponent,
    LoadingPanelComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    RouterModule.forRoot(
      appRoutes
    ),
    ModalModule.forRoot(),
    BrowserAnimationsModule,
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
    MatDialogModule,
    MatCardModule,
    MatIconModule,
    MatProgressBarModule
  ],
  bootstrap: [AppComponent],
  providers: [
    { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 2500 } },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizeInterceptor,
      multi: true
    }
  ]
})
export class AppModule { }

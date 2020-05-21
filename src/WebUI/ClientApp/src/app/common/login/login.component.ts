import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { AuthenticationClient, AuthenticateUserCommand } from 'src/app/tti_graduation_work-api';
import { NotificationService } from 'src/app/services/notification.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [NotificationService, UserService, AuthService]
})
export class LoginComponent implements OnInit {

  form: FormGroup;
  public loginInvalid: boolean;
  private formSubmitAttempt: boolean;
  private returnUrl: string;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private authenticationClient: AuthenticationClient,
    private notificationService: NotificationService,
    private userService: UserService
  ) {

  }


  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/home';
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    if (this.authService.checkAuthenticated()) {
      this.router.navigate([this.returnUrl]);
    }
  }


  onSubmit() {

    this.loginInvalid = false;

    this.formSubmitAttempt = false;

    if (this.form.valid) {
      try {
        const username = this.form.get('username').value;
        const password = this.form.get('password').value;
        this.authenticationClient.login(AuthenticateUserCommand.fromJS({ username: username, password: password })).subscribe(result => {
          if (result) {
            this.userService.storeToken(result.token);
            this.router.navigate([this.returnUrl]);
          }
        },
        error => {
          this.notificationService.error(error);
        });

      } catch (err) {
        this.loginInvalid = true;
      }
    } else {
      this.formSubmitAttempt = true;
    }
  }
}

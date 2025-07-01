import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../../shared/services/auth.service';
import { UserLogin, UserResponse } from '../../../shared/models/User.model';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  submitted = false;
  loginForm!: FormGroup;
  errorMsg: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  public onSubmit() {
    console.log(this.loginForm.value);

    this.authService.login(this.loginForm.value as UserLogin).subscribe({
      next: (response: UserResponse) => {
        // const { username, email, token }: UserResponse = response;
        this.authService.setCurrentUser(response);
        this.router.navigate(['home']);
      },
      error: (error: HttpErrorResponse) => {
        console.error(error);
        this.errorMsg = error.error;
        setTimeout(() => {
          this.errorMsg = '';
        }, 5000);
      },
    });
  }

  public goToRegister() {
    this.router.navigate(['register']);
  }

  public goBack() {
    this.location.back();
  }
}

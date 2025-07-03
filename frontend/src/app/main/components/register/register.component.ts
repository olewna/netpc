import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../shared/services/auth.service';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { UserRegister, UserResponse } from '../../../shared/models/User.model';
import { HttpErrorResponse } from '@angular/common/http';
import { passwordConfirmValidator } from '../../../shared/directives/passwordConfirm.directive';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent implements OnInit {
  protected submitted = false;
  protected registerForm!: FormGroup;
  protected errorMsg: string = '';
  protected showPassword: boolean = false;
  protected showPasswordConfirmation: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group(
      {
        username: ['', [Validators.required, Validators.minLength(3)]],
        email: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(8),
            Validators.pattern(
              /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,}$/
            ),
          ],
        ],
        confirmPassword: ['', [Validators.required]],
      },
      {
        validators: [passwordConfirmValidator],
      }
    );
  }

  public onSubmit() {
    console.log(this.registerForm.value);

    this.authService
      .register(this.registerForm.value as UserRegister)
      .subscribe({
        next: (response: UserResponse) => {
          // const { username, email, token }: UserResponse = response;
          this.authService.setCurrentUser(response);
          this.router.navigate(['home']);
        },
        error: (error: HttpErrorResponse) => {
          console.error(error);
          this.errorMsg = error.error ? error.error.message : 'Błąd';
          setTimeout(() => {
            this.errorMsg = '';
          }, 5000);
        },
      });
  }

  public goToLogin() {
    this.router.navigate(['login']);
  }

  public goBack() {
    this.location.back();
  }

  public togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  public togglePasswordConfirmation(): void {
    this.showPasswordConfirmation = !this.showPasswordConfirmation;
  }
}

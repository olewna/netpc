import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../shared/services/auth.service';
import { UserResponse } from '../../../shared/models/User.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent implements OnInit, OnDestroy {
  public constructor(
    private router: Router,
    private authService: AuthService
  ) {}

  isLoggedIn = false;
  private sub!: Subscription;

  public ngOnInit(): void {
    this.sub = this.authService.isLoggedIn$.subscribe((status) => {
      console.log(status);
      this.isLoggedIn = status;
    });
  }

  public goToHome() {
    this.router.navigate(['home']);
  }

  public goToLogin() {
    this.router.navigate(['login']);
  }

  public logout() {
    this.authService.logout();
    // this.router.navigate(['login']);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}

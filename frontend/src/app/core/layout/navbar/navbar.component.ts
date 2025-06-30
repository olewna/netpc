import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  public constructor(private router: Router) {}

  public goToHome() {
    this.router.navigate(['home']);
  }

  // public goToLogin() {
  //   this.router.navigate(['login'])
  // }
}

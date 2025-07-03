import { Component, OnDestroy, OnInit } from '@angular/core';
import { ContactService } from '../../../shared/services/contact.service';
import { Contact } from '../../../shared/models/Contact.model';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../shared/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit, OnDestroy {
  public constructor(
    private contactService: ContactService,
    private router: Router,
    private authService: AuthService
  ) {}

  public contacts: Contact[] = [];

  isLoggedIn = false;
  private sub!: Subscription;

  public ngOnInit(): void {
    this.contactService.getAllContacts().subscribe({
      next: (data) => (this.contacts = data), // TODO dodać loading
      error: (err) => console.error('Błąd podczas pobierania kontaktów:', err),
    });

    this.sub = this.authService.isLoggedIn$.subscribe((status) => {
      console.log(status);
      this.isLoggedIn = status;
    });
  }

  public goToDetails(id: string) {
    this.router.navigate(['contact', id]);
  }

  public goToContactAddForm() {
    this.router.navigate(['contact', 'form']);
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}

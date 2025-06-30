import { Component, OnInit } from '@angular/core';
import { ContactService } from '../../../shared/services/contact.service';
import { Contact } from '../../../shared/models/Contact.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  public constructor(
    private contactService: ContactService,
    private router: Router
  ) {}

  public contacts: Contact[] = [];

  public ngOnInit(): void {
    this.contactService.getAllContacts().subscribe({
      next: (data) => (this.contacts = data),
      error: (err) => console.error('Błąd podczas pobierania kontaktów:', err),
    });
  }

  public goToDetails(id: string) {
    this.router.navigate(['contact', id]);
  }
}

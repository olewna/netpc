import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ContactService } from '../../../shared/services/contact.service';
import { Contact } from '../../../shared/models/Contact.model';
import { Location } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss',
})
export class ContactComponent implements OnInit {
  public constructor(
    private activatedRoute: ActivatedRoute,
    private contactService: ContactService,
    private location: Location,
    private router: Router
  ) {}

  protected contactId!: string;
  protected showDeleteModal: boolean = false;
  protected showResponseModal: boolean = false;
  protected responseModalMsg: string = '';
  protected errorMsg: string = '';
  protected contact: Contact = {
    id: '',
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    category: '',
    subCategory: '',
    phoneNumber: '',
    dateOfBirth: '',
  };

  public ngOnInit(): void {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.contactId = params['id'];
    });

    this.contactService.getContactById(this.contactId).subscribe({
      next: (res: Contact) => {
        this.contact = res;
      },
      error: (err: HttpErrorResponse) => {
        this.errorMsg = 'Nie ma takiego kontaktu!';
      },
    });
  }

  public goBack(): void {
    this.location.back();
  }

  public goToEditContact() {
    this.router.navigate(['contact', 'form', this.contact.id]);
  }

  public showModal(): void {
    this.showDeleteModal = true;
  }

  public cancel(): void {
    this.showDeleteModal = false;
  }

  public confirm(): void {
    this.contactService.deleteContact(this.contact.id).subscribe({
      next: () => {
        this.showDeleteModal = false;
        this.showResponseModal = true;
        this.responseModalMsg = 'Sukces! Poprawnie usunięto kontakt';
      },
      error: (err: HttpErrorResponse) => {
        this.showDeleteModal = false;
        this.showResponseModal = true;
        this.responseModalMsg = 'Nie udało się usunąć kontaktu';
        console.error(err);
      },
    });
  }

  public closeResponseModal(): void {
    this.router.navigate(['home']);
  }
}

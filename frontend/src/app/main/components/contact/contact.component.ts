import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ContactService } from '../../../shared/services/contact.service';
import { Contact } from '../../../shared/models/Contact.model';
import { Location } from '@angular/common';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss',
})
export class ContactComponent implements OnInit {
  public constructor(
    private activatedRoute: ActivatedRoute,
    private contactService: ContactService,
    private location: Location
  ) {}

  protected contactId!: string;
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

    this.contactService
      .getContactById(this.contactId)
      .subscribe((res: Contact) => {
        this.contact = res;
      });
  }

  public goBack(): void {
    this.location.back();
  }
}

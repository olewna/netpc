import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Contact } from '../models/Contact.model';
import { Observable } from 'rxjs';
import { ContactDto } from '../form.models/ContactDto.model';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  public constructor(private httpClient: HttpClient) {}

  public getAllContacts(): Observable<Contact[]> {
    return this.httpClient.get<Contact[]>(`api/contact`);
  }

  public getContactById(id: string): Observable<Contact> {
    return this.httpClient.get<Contact>(`api/contact/` + id);
  }

  public deleteContact(id: string): Observable<void> {
    return this.httpClient.delete<void>(`api/contact/` + id);
  }

  public createContact(contact: ContactDto): Observable<Contact> {
    return this.httpClient.post<Contact>(`api/contact`, contact);
  }
}

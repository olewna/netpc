import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { RegisterComponent } from './components/register/register.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { LoginComponent } from './components/login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './components/home/home.component';
import { ContactFormComponent } from './components/contact-form/contact-form.component';
import { ContactComponent } from './components/contact/contact.component';

@NgModule({
  declarations: [
    RegisterComponent,
    NotFoundComponent,
    LoginComponent,
    HomeComponent,
    ContactFormComponent,
    ContactComponent,
  ],
  imports: [CommonModule, SharedModule, ReactiveFormsModule],
})
export class MainModule {}

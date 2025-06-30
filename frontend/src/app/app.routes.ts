import { Routes } from '@angular/router';
import { HomeComponent } from './main/components/home/home.component';
import { ContactComponent } from './main/components/contact/contact.component';
import { NotFoundComponent } from './main/components/not-found/not-found.component';
import { ContactFormComponent } from './main/components/contact-form/contact-form.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: HomeComponent,
  },
  {
    path: 'home',
    redirectTo: '',
  },
  {
    path: 'contact',
    component: ContactComponent,
  },
  {
    path: 'contact/:id',
    component: ContactFormComponent,
  },
  //   {
  //     path: 'register',
  //     component: undefined,
  //   },
  //   {
  //     path: 'login',
  //     component: undefined,
  //   },
  {
    path: '**',
    component: NotFoundComponent,
  },
];

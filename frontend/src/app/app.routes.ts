import { Routes } from '@angular/router';
import { HomeComponent } from './main/components/home/home.component';
import { ContactComponent } from './main/components/contact/contact.component';
import { NotFoundComponent } from './main/components/not-found/not-found.component';
import { ContactFormComponent } from './main/components/contact-form/contact-form.component';
import { RegisterComponent } from './main/components/register/register.component';
import { LoginComponent } from './main/components/login/login.component';
import { loggedGuard, notLoggedGuard } from './core/guards/auth-guard.guard';

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
    path: 'contact/form',
    component: ContactFormComponent,
    canActivate: [notLoggedGuard],
  },
  {
    path: 'contact/form/:id',
    component: ContactFormComponent,
    canActivate: [notLoggedGuard],
  },
  {
    path: 'contact/:id',
    component: ContactComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [loggedGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [loggedGuard],
  },
  {
    path: '**',
    component: NotFoundComponent,
  },
];

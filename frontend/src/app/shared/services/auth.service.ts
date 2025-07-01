import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserLogin, UserRegister, UserResponse } from '../models/User.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private httpClient: HttpClient) {}
  private loggedIn = new BehaviorSubject<boolean>(this.isLoggedIn());
  isLoggedIn$ = this.loggedIn.asObservable();

  public login(user: UserLogin): Observable<UserResponse> {
    return this.httpClient.post<UserResponse>(`api/account/login`, user);
  }

  public register(user: UserRegister): Observable<UserResponse> {
    return this.httpClient.post<UserResponse>(`api/account/register`, user);
  }

  get token(): string | null {
    return localStorage.getItem('token');
  }

  public setCurrentUser(user: UserResponse) {
    localStorage.setItem('token', user.token);
    this.loggedIn.next(true);
  }

  public logout() {
    localStorage.removeItem('token');
    this.loggedIn.next(false);
  }

  public isLoggedIn(): boolean {
    const token = this.token;
    if (!token) return false;

    try {
      const jwt = jwtDecode(token);
      console.log(jwt);
      const timeLeft = jwt.exp;
      return timeLeft! * 1000 > Date.now();
    } catch {
      return false;
    }
  }
}

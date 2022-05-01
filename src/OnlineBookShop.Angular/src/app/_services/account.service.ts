import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserForLogin } from '../_models/Account/UserForLogin';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { BearerToken } from '../_models/Account/BearerToken';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl + 'account/';

  constructor(private http: HttpClient) { }

  isLoggedIn(): boolean{
    const token = localStorage.getItem('accessToken');
    return token ? true : false;
  }

  login(userForLoginDto: UserForLogin): Observable<BearerToken> {
    return this.http.post<BearerToken>(this.baseUrl + 'login/', userForLoginDto);
  }
}

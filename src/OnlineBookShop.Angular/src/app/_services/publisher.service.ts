import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Publisher } from '../_models/Publishers/Publisher';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PublisherService {

  baseUrl = environment.apiUrl;

  constructor(
    private http: HttpClient
  ) { }

  getAllPublishers(): Observable<Publisher[]> {
    return this.http.get<Publisher[]>(this.baseUrl + 'publishers');
  }

}

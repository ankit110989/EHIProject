import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Contact } from '../models/contact';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private apiURL = environment.baseApiUrl + "contact";

  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<Contact[]> {
    return this.httpClient.get<any>(this.apiURL);
  }

  create(data): Observable<Contact> {
    return this.httpClient.post<Contact>(this.apiURL, JSON.stringify(data))
  }

  find(id): Observable<Contact> {
    return this.httpClient.get<Contact>(this.apiURL + '/' + id);
  }

  update(id, Contact): Observable<Contact> {
    return this.httpClient.put<Contact>(this.apiURL + '/' + id, JSON.stringify(Contact))
  }

  delete(id) {
    return this.httpClient.delete<any>(this.apiURL + '/' + id)
  }

  changeStatus(id) {
    return this.httpClient.post<Contact>(this.apiURL + '/ChangeContactStatus/' + id, null);
  }

}



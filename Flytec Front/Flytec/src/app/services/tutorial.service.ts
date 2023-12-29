import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tutorial } from '../models/tutorial.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://localhost:7221/api/v1/auth/';

@Injectable({
  providedIn: 'root'
})
export class TutorialService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Tutorial[]> {
    return this.http.get<Tutorial[]>(baseUrl + "users");
  }

  get(id: any): Observable<Tutorial> {
    return this.http.get<Tutorial>(baseUrl + "GetUserById?userId=" + id);
  }

  create(data: any): Observable<any> {
    return this.http.post(baseUrl + "registerUser", data);
  }

  update(id: any, name: any, email: any, password: any): Observable<any> {
    return this.http.post(baseUrl + "UpdateUser?id=" + id,
    {
      name,
      email,
      password
    },
    httpOptions
    );
  }

  delete(id: any): Observable<any> {
    return this.http.delete(baseUrl + "RemoveUser?userId=" + id);
  }

  deleteAll(): Observable<any> {
    return this.http.delete(baseUrl);
  }

  findByTitle(title: any): Observable<Tutorial[]> {
    return this.http.get<Tutorial[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }
}
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Adjuvante } from '../../models/adjuvante/adjuvante.model';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/adjuvante/';
@Injectable({
  providedIn: 'root'
})
export class AdjuvanteService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Adjuvante[]> {
    return this.http.get<Adjuvante[]>(baseUrl);
  }

  get(id: any): Observable<Adjuvante> {
    debugger;
    return this.http.get<Adjuvante>(baseUrl + id);
  }

  create(data: Adjuvante): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: Adjuvante): Observable<any> {
    debugger;
    return this.http.put(baseUrl + id, data,
    httpOptions
    );
  }

  delete(id: any): Observable<any> {
    debugger;
    return this.http.delete(baseUrl + id);
  }

  deleteAll(): Observable<any> {
    return this.http.delete(baseUrl);
  }

  findByTitle(title: any): Observable<Adjuvante[]> {
    return this.http.get<Adjuvante[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownData(): Observable<any[]> {
    return this.http.get<any[]>(baseUrl);
  }
}

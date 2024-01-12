import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pista } from '../../models/pista/pista.model';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/pista/';

@Injectable({
  providedIn: 'root'
})
export class PistaService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Pista[]> {
    return this.http.get<Pista[]>(baseUrl);
  }

  get(id: any): Observable<Pista> {
    debugger;
    return this.http.get<Pista>(baseUrl + id);
  }

  create(data: Pista): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: Pista): Observable<any> {
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

  findByTitle(title: any): Observable<Pista[]> {
    return this.http.get<Pista[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownData(): Observable<any[]> {
    return this.http.get<any[]>(baseUrl);
  }
}

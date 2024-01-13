import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../../models/cliente/cliente.model';
import { Bula } from '../../models/bula/bula.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/bula/';
const dropdownCultura = 'https://flytec.keltecnologia.com.br/api/v1/cultura/';
const dropdownAlvoBiologico = 'https://flytec.keltecnologia.com.br/api/v1/alvoBiologico/';

@Injectable({
  providedIn: 'root'
})
export class BulaService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Bula[]> {
    return this.http.get<Bula[]>(baseUrl);
  }

  get(id: any): Observable<Bula> {
    debugger;
    return this.http.get<Bula>(baseUrl + id);
  }

  create(data: Bula): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: Bula): Observable<any> {
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

  findByTitle(title: any): Observable<Bula[]> {
    return this.http.get<Bula[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownAlvoBiologico(): Observable<any[]> {
    return this.http.get<any[]>(dropdownAlvoBiologico);
  }

  getDropdownCultura(): Observable<any[]> {
    return this.http.get<any[]>(dropdownCultura);
  }
}

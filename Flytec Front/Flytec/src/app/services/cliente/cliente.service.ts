import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../../models/cliente/cliente.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/cliente/';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  constructor(private http: HttpClient) { }

  getAll(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(baseUrl);
  }

  get(id: any): Observable<Cliente> {
    debugger;
    return this.http.get<Cliente>(baseUrl + id);
  }

  create(data: Cliente): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: Cliente): Observable<any> {
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

  findByTitle(title: any): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownData(): Observable<any[]> {
    return this.http.get<any[]>(baseUrl);
  }
}

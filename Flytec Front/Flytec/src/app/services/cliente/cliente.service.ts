import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../../models/cliente/cliente.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://localhost:7221/api/v1/cliente/';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  constructor(private http: HttpClient) { }

  getAll(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(baseUrl + "clientes");
  }

  get(id: any): Observable<Cliente> {
    debugger;
    return this.http.get<Cliente>(baseUrl + "GetClienteById?id=" + id);
  }

  create(data: Cliente): Observable<any> {
    debugger;
    return this.http.post(baseUrl + "CriarCliente", data,
    httpOptions);
  }

  update(id: any, data: Cliente): Observable<any> {
    debugger;
    return this.http.post(baseUrl + "UpdateCliente?id=" + id, data,
    httpOptions
    );
  }

  delete(id: any): Observable<any> {
    debugger;
    return this.http.delete(baseUrl + "RemoveCliente?id=" + id);
  }

  deleteAll(): Observable<any> {
    return this.http.delete(baseUrl);
  }

  findByTitle(title: any): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownData(): Observable<any[]> {
    return this.http.get<any[]>(baseUrl + "getAll");
  }
}

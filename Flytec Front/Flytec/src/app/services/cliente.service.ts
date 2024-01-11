import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../models/cliente/cliente.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://localhost:7221/api/v1/clientes/';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  constructor(private http: HttpClient) { }

  getAll(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(baseUrl + "getAll");
  }

  get(id: any): Observable<Cliente> {
    return this.http.get<Cliente>(baseUrl + "GetClienteById?clienteId=" + id);
  }

  create(data: any): Observable<any> {
    return this.http.post(baseUrl + "createCliente", data);
  }

  update(id: any, name: any, email: any, password: any): Observable<any> {
    return this.http.post(baseUrl + "UpdateCliente?id=" + id,
    {
      name,
      email,
      password
    },
    httpOptions
    );
  }

  delete(id: any): Observable<any> {
    return this.http.delete(baseUrl + "RemoveCliente?clienteId=" + id);
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

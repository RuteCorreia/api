import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../../models/cliente/cliente.model';
import { Produto } from '../../models/produto/produto.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/produto/';
const dropdownDataUrl = 'https://flytec.keltecnologia.com.br/api/v1/cultura/';


@Injectable({
  providedIn: 'root'
})

export class ProdutoService {
  constructor(private http: HttpClient) { }

  getAll(): Observable<Produto[]> {
    return this.http.get<Produto[]>(baseUrl);
  }

  get(id: any): Observable<Produto> {
    debugger;
    return this.http.get<Produto>(baseUrl + id);
  }

  create(data: Produto): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: Produto): Observable<any> {
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
    debugger
    return this.http.get<any[]>(dropdownDataUrl);
  }
}

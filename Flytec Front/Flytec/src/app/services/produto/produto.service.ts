import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../../models/cliente/cliente.model';
import { Produto } from '../../models/produto/produto.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://localhost:7221/api/v1/produto/';
const dropdownDataUrl = 'https://localhost:7221/api/v1/cultura/';


@Injectable({
  providedIn: 'root'
})

export class ProdutoService {
  constructor(private http: HttpClient) { }

  getAll(): Observable<Produto[]> {
    return this.http.get<Produto[]>(baseUrl + "produtos");
  }

  get(id: any): Observable<Produto> {
    debugger;
    return this.http.get<Produto>(baseUrl + "GetProdutoById?id=" + id);
  }

  create(data: Produto): Observable<any> {
    debugger;
    return this.http.post(baseUrl + "CriarProduto", data,
    httpOptions);
  }

  update(id: any, data: Produto): Observable<any> {
    debugger;
    return this.http.post(baseUrl + "UpdateProduto?id=" + id, data,
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
    debugger
    return this.http.get<any[]>(dropdownDataUrl + "culturas");
  }
}

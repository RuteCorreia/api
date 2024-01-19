import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../../models/cliente/cliente.model';
import { Aplicacao } from '../../models/aplicacao/aplicacao.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/aplicacao/';
const dropdownEmpresa = 'https://flytec.keltecnologia.com.br/api/v1/empresa/';
const dropdownPiloto = 'https://flytec.keltecnologia.com.br/api/v1/piloto/';
const dropdownExecutor = 'https://flytec.keltecnologia.com.br/api/v1/executor/';
const dropdownCliente = 'https://flytec.keltecnologia.com.br/api/v1/cliente/';
const dropdownCultura = 'https://flytec.keltecnologia.com.br/api/v1/cultura/';


@Injectable({
  providedIn: 'root'
})
export class AplicacaoService {


  constructor(private http: HttpClient) { }

  getAll(): Observable<Aplicacao[]> {
    return this.http.get<Aplicacao[]>(baseUrl);
  }

  get(id: any): Observable<Aplicacao> {
    debugger;
    return this.http.get<Aplicacao>(baseUrl + id);
  }

  create(data: Aplicacao): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: Aplicacao): Observable<any> {
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

  findByTitle(title: any): Observable<Aplicacao[]> {
    return this.http.get<Aplicacao[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownEmpresa(): Observable<any[]> {
    return this.http.get<any[]>(dropdownEmpresa);
  }
  getDropdownPiloto(): Observable<any[]> {
    return this.http.get<any[]>(dropdownPiloto);
  }
    getDropdownExecutor(): Observable<any[]> {
    return this.http.get<any[]>(dropdownExecutor);
  }
    getDropdownCliente(): Observable<any[]> {
    return this.http.get<any[]>(dropdownCliente);
  } 
   getDropdownCultura(): Observable<any[]> {
    return this.http.get<any[]>(dropdownCultura);
  }
}

import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../../models/cliente/cliente.model';
import { ClassificacaoToxicologica } from '../../models/classificacao-toxicologica/classificacao-toxicologica.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/classificacaoToxicologica/';

@Injectable({
  providedIn: 'root'
})
export class ClassificacaoToxicologicaService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<ClassificacaoToxicologica[]> {
    return this.http.get<ClassificacaoToxicologica[]>(baseUrl);
  }

  get(id: any): Observable<ClassificacaoToxicologica> {
    debugger;
    return this.http.get<ClassificacaoToxicologica>(baseUrl + id);
  }

  create(data: ClassificacaoToxicologica): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: ClassificacaoToxicologica): Observable<any> {
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

  findByTitle(title: any): Observable<ClassificacaoToxicologica[]> {
    return this.http.get<ClassificacaoToxicologica[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownData(): Observable<any[]> {
    return this.http.get<any[]>(baseUrl);
  }
}

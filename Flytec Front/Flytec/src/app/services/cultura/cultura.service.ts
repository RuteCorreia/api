import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../../models/cliente/cliente.model';
import { Cultura } from '../../models/cultura/cultura.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/cultura/';

@Injectable({
  providedIn: 'root'
})
export class CulturaService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Cultura[]> {
    return this.http.get<Cultura[]>(baseUrl);
  }

  get(id: any): Observable<Cultura> {
    debugger;
    return this.http.get<Cultura>(baseUrl + id);
  }

  create(data: Cultura): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: Cultura): Observable<any> {
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

  findByTitle(title: any): Observable<Cultura[]> {
    return this.http.get<Cultura[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownData(): Observable<any[]> {
    return this.http.get<any[]>(baseUrl);
  }
}

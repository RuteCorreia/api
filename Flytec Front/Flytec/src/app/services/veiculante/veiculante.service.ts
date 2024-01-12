import { Injectable } from '@angular/core';
import { Veiculante } from '../../models/veiculante/veiculante.model';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/veiculante/';

@Injectable({
  providedIn: 'root'
})
export class VeiculanteService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Veiculante[]> {
    return this.http.get<Veiculante[]>(baseUrl);
  }

  get(id: any): Observable<Veiculante> {
    debugger;
    return this.http.get<Veiculante>(baseUrl + id);
  }

  create(data: Veiculante): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: Veiculante): Observable<any> {
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

  findByTitle(title: any): Observable<Veiculante[]> {
    return this.http.get<Veiculante[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownData(): Observable<any[]> {
    return this.http.get<any[]>(baseUrl);
  }
}

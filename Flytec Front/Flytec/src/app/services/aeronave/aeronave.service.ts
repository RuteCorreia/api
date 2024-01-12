import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../../models/cliente/cliente.model';
import { Aeronave } from '../../models/aeronave/aeronave.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/aeronave/';

@Injectable({
  providedIn: 'root'
})
export class AeronaveService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Aeronave[]> {
    return this.http.get<Aeronave[]>(baseUrl);
  }

  get(id: any): Observable<Aeronave> {
    debugger;
    return this.http.get<Aeronave>(baseUrl + id);
  }

  create(data: Aeronave): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: Aeronave): Observable<any> {
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

  findByTitle(title: any): Observable<Aeronave[]> {
    return this.http.get<Aeronave[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownData(): Observable<any[]> {
    return this.http.get<any[]>(baseUrl);
  }
}

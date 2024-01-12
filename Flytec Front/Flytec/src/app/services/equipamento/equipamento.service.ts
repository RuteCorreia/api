import { Injectable } from '@angular/core';
import { Equipamento } from '../../models/equipamento/equipamento.model';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/equipamento/';

@Injectable({
  providedIn: 'root'
})
export class EquipamentoService {


  constructor(private http: HttpClient) { }

  getAll(): Observable<Equipamento[]> {
    return this.http.get<Equipamento[]>(baseUrl);
  }

  get(id: any): Observable<Equipamento> {
    debugger;
    return this.http.get<Equipamento>(baseUrl + id);
  }

  create(data: Equipamento): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: Equipamento): Observable<any> {
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

  findByTitle(title: any): Observable<Equipamento[]> {
    return this.http.get<Equipamento[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownData(): Observable<any[]> {
    return this.http.get<any[]>(baseUrl);
  }
}

import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Veiculante } from '../../../models/veiculante/veiculante.model';
import { VeiculanteService } from '../../../services/veiculante/veiculante.service';
import { Empresa } from '../../../models/empresa/empresa.model';
import { EmpresaService } from '../../../services/empresa/empresa.service';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-empresa-details',
  templateUrl: './empresa-details.component.html',
  styleUrl: './empresa-details.component.css'
})
export class EmpresaDetailsComponent implements OnInit{

  @Input() viewMode = false;

  @Input() currentEmpresa: Empresa = {
    idEmpresa: 0,
    nome: '',
    imagem: '',
    planoContratado: 1,

  };
  
  message = '';

  constructor(
    private empresaService: EmpresaService,
    private route: ActivatedRoute,
    private router: Router) { }


  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getEmpresa(this.route.snapshot.params["id"]);
    }
  }

  getEmpresa(id: number): void {
    debugger;
      this.empresaService.get(id)
      .subscribe({
        next: (data) => {
          this.currentEmpresa = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateEmpresa(): void {
    debugger;
    this.message = '';

    this.empresaService.update(this.currentEmpresa.idEmpresa,this.currentEmpresa)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/empresa";
        },
        error: (e) => console.error(e)
      });
  }

  deleteEmpresa(): void {
    debugger;
    this.empresaService.delete(this.currentEmpresa.idEmpresa)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/empresa";
        },
        error: (e) => console.error(e)
      });
  }
}

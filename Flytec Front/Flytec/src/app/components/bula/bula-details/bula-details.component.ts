import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Aplicacao } from '../../../models/aplicacao/aplicacao.model';
import { AplicacaoService } from '../../../services/aplicacao/aplicacao.service';
import { Bula } from '../../../models/bula/bula.model';
import { BulaService } from '../../../services/bula/bula.service';

@Component({
  selector: 'app-bula-details',
  templateUrl: './bula-details.component.html',
  styleUrl: './bula-details.component.css'
})
export class BulaDetailsComponent implements OnInit{
  voltar() {
    window.location.href = "/bula";
    }
      dropdownDataAlvoBiologico!: any[];
      dropdownDataCultura!: any[];

      selectedItem: any;
      
  @Input() viewMode = false;

  @Input() currentBula: Bula = {
    idBula: 0,
    nomeProduto: '',
    idCultura: 0,
    idClassificacaoToxicologica: 0,
    classe: '',
    tipoDeFormulacao: '',
    idAlvoBiologico: 0,
    doseProdutoComercial: 0,
    adjuvante: '',
    idTipoDeServico: 0,
  };
  
  message = '';

  constructor(
    private bulaService: BulaService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getBula(this.route.snapshot.params["id"]);
        this.bulaService.getDropdownCultura().subscribe(data => {
          this.dropdownDataCultura = data;
        });
        this.bulaService.getDropdownAlvoBiologico().subscribe(data => {
          this.dropdownDataAlvoBiologico = data;
        });
    }
  }

  getBula(id: number): void {
    debugger;
      this.bulaService.get(id)
      .subscribe({
        next: (data) => {
          this.currentBula = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateBula(): void {
    debugger;
    this.message = '';

    this.bulaService.update(this.currentBula.idBula,this.currentBula)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/bula";
        },
        error: (e) => console.error(e)
      });
  }

  deleteBula(): void {
    debugger;
    this.bulaService.delete(this.currentBula.idBula)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/bula";
        },
        error: (e) => console.error(e)
      });
  }
}

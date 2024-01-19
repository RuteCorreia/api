import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Aeronave } from '../../../models/aeronave/aeronave.model';
import { AeronaveService } from '../../../services/aeronave/aeronave.service';
import { Aplicacao } from '../../../models/aplicacao/aplicacao.model';
import { AplicacaoService } from '../../../services/aplicacao/aplicacao.service';

@Component({
  selector: 'app-aplicacao-details',
  templateUrl: './aplicacao-details.component.html',
  styleUrl: './aplicacao-details.component.css'
})
export class AplicacaoDetailsComponent implements OnInit{

  voltar() {
    window.location.href = "/aplicacao";
    }
    
      dropdownDataEmpresa!: any[];
      dropdownDataPiloto!: any[];
      dropdownDataExecutor!: any[];
      dropdownDataCliente!: any[];
      dropdownDataCultura!: any[];

      selectedItem: any;
      
  @Input() viewMode = false;

  @Input() currentAplicacao: Aplicacao = {
      id: 0,
      idEmpresa: 0,
      statusEnvio: '',
      idPiloto: 0,
      idExecutor: 0,
      idCliente: 0,
      idCultura: 0,
  };
  
  message = '';

  constructor(
    private aplicacaoService: AplicacaoService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getAplicacao(this.route.snapshot.params["id"]);
        this.aplicacaoService.getDropdownCliente().subscribe(data => {
          this.dropdownDataCliente = data;
        });
        this.aplicacaoService.getDropdownCultura().subscribe(data => {
          this.dropdownDataCultura = data;
        });
        this.aplicacaoService.getDropdownEmpresa().subscribe(data => {
          this.dropdownDataEmpresa = data;
        });
        this.aplicacaoService.getDropdownExecutor().subscribe(data => {
          this.dropdownDataExecutor = data;
        });
        this.aplicacaoService.getDropdownPiloto().subscribe(data => {
          this.dropdownDataPiloto = data;
        });
    }
  }

  getAplicacao(id: number): void {
    debugger;
      this.aplicacaoService.get(id)
      .subscribe({
        next: (data) => {
          this.currentAplicacao = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateAplicacao(): void {
    debugger;
    this.message = '';

    this.aplicacaoService.update(this.currentAplicacao.id,this.currentAplicacao)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/aplicacao";
        },
        error: (e) => console.error(e)
      });
  }

  deleteAplicacao(): void {
    debugger;
    this.aplicacaoService.delete(this.currentAplicacao.id)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/aplicacao";
        },
        error: (e) => console.error(e)
      });
  }
}

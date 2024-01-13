import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Adjuvante } from '../../../models/adjuvante/adjuvante.model';
import { AdjuvanteService } from '../../../services/adjuvante/adjuvante.service';
import { Veiculante } from '../../../models/veiculante/veiculante.model';
import { VeiculanteService } from '../../../services/veiculante/veiculante.service';

@Component({
  selector: 'app-veiculante-details',
  templateUrl: './veiculante-details.component.html',
  styleUrl: './veiculante-details.component.css'
})
export class VeiculanteDetailsComponent implements OnInit{
  @Input() viewMode = false;

  @Input() currentVeiculante: Veiculante = {
    idVeiculante: 0,
    nome: '',
  };
  
  message = '';

  constructor(
    private veiculanteService: VeiculanteService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getVeiculante(this.route.snapshot.params["id"]);
    }
  }

  getVeiculante(id: number): void {
    debugger;
      this.veiculanteService.get(id)
      .subscribe({
        next: (data) => {
          this.currentVeiculante = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateVeiculante(): void {
    debugger;
    this.message = '';

    this.veiculanteService.update(this.currentVeiculante.idVeiculante,this.currentVeiculante)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/veiculante";
        },
        error: (e) => console.error(e)
      });
  }

  deleteVeiculante(): void {
    debugger;
    this.veiculanteService.delete(this.currentVeiculante.idVeiculante)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/veiculante";
        },
        error: (e) => console.error(e)
      });
  }
}

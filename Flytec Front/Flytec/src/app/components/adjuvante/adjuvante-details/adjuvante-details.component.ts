import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlvoBiologico } from '../../../models/alvo-biologico/alvo-biologico.model';
import { AlvoBiologicoService } from '../../../services/alvo-biologico/alvo-biologico.service';
import { Adjuvante } from '../../../models/adjuvante/adjuvante.model';
import { AdjuvanteService } from '../../../services/adjuvante/adjuvante.service';

@Component({
  selector: 'app-adjuvante-details',
  templateUrl: './adjuvante-details.component.html',
  styleUrl: './adjuvante-details.component.css'
})
export class AdjuvanteDetailsComponent implements OnInit{

  @Input() viewMode = false;

  @Input() currentAdjuvante: Adjuvante = {
    id: 0,
    nome: '',
  };
  
  message = '';

  constructor(
    private adjuvanteService: AdjuvanteService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getAdjuvante(this.route.snapshot.params["id"]);
    }
  }

  getAdjuvante(id: number): void {
    debugger;
      this.adjuvanteService.get(id)
      .subscribe({
        next: (data) => {
          this.currentAdjuvante = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateAdjuvante(): void {
    debugger;
    this.message = '';

    this.adjuvanteService.update(this.currentAdjuvante.id,this.currentAdjuvante)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/adjuvante";
        },
        error: (e) => console.error(e)
      });
  }

  deleteAdjuvante(): void {
    debugger;
    this.adjuvanteService.delete(this.currentAdjuvante.id)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/adjuvante";
        },
        error: (e) => console.error(e)
      });
  }
}

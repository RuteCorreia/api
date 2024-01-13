import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlvoBiologico } from '../../../models/alvo-biologico/alvo-biologico.model';
import { AlvoBiologicoService } from '../../../services/alvo-biologico/alvo-biologico.service';
import { Cultura } from '../../../models/cultura/cultura.model';
import { CulturaService } from '../../../services/cultura/cultura.service';

@Component({
  selector: 'app-cultura-details',
  templateUrl: './cultura-details.component.html',
  styleUrl: './cultura-details.component.css'
})
export class CulturaDetailsComponent implements OnInit{
  dropdownData!: any[];
  selectedItem: any;

  @Input() viewMode = false;

  @Input() currentCultura: Cultura = {
    idCultura: 0,
    nome: '',
    alvoBiologico: 'SemDado',
  };
  
  message = '';

  constructor(
    private culturaService: CulturaService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getCultura(this.route.snapshot.params["id"]);
    }
  }

  getCultura(id: number): void {
    debugger;
      this.culturaService.get(id)
      .subscribe({
        next: (data) => {
          this.currentCultura = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateCultura(): void {
    debugger;
    this.message = '';

    this.culturaService.update(this.currentCultura.idCultura,this.currentCultura)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/cultura";
        },
        error: (e) => console.error(e)
      });
  }

  deleteCultura(): void {
    debugger;
    this.culturaService.delete(this.currentCultura.idCultura)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/cultura";
        },
        error: (e) => console.error(e)
      });
  }
}

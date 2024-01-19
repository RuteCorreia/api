import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Aeronave } from '../../../models/aeronave/aeronave.model';
import { AeronaveService } from '../../../services/aeronave/aeronave.service';
import { AlvoBiologico } from '../../../models/alvo-biologico/alvo-biologico.model';
import { AlvoBiologicoService } from '../../../services/alvo-biologico/alvo-biologico.service';

@Component({
  selector: 'app-alvo-biologico-details',
  templateUrl: './alvo-biologico-details.component.html',
  styleUrl: './alvo-biologico-details.component.css'
})
export class AlvoBiologicoDetailsComponent implements OnInit {
  dropdownData!: any[];
  selectedItem: any;

  @Input() viewMode = false;

  @Input() currentAlvoBiologico: AlvoBiologico = {
    idProduto: 0,
    nome: '',
    doseProdutoPorHectare: '',
  };
  
  message = '';

  constructor(
    private alvoBiologicoService: AlvoBiologicoService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getAlvoBiologico(this.route.snapshot.params["id"]);
      this.alvoBiologicoService.getDropdownData().subscribe(data => {
        this.dropdownData = data;
        debugger
      });
    }
  }

  getAlvoBiologico(id: number): void {
    debugger;
      this.alvoBiologicoService.get(id)
      .subscribe({
        next: (data) => {
          this.currentAlvoBiologico = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateAlvoBiologico(): void {
    debugger;
    this.message = '';

    this.alvoBiologicoService.update(this.currentAlvoBiologico.id,this.currentAlvoBiologico)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/alvoBiologico";
        },
        error: (e) => console.error(e)
      });
  }

  deleteAlvoBiologico(): void {
    debugger;
    this.alvoBiologicoService.delete(this.currentAlvoBiologico.id)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/alvoBiologico";
        },
        error: (e) => console.error(e)
      });
  }
}

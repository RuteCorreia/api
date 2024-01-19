import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Produto } from '../../../models/produto/produto.model';
import { ProdutoService } from '../../../services/produto/produto.service';
import { Pista } from '../../../models/pista/pista.model';
import { PistaService } from '../../../services/pista/pista.service';

@Component({
  selector: 'app-pista-details',
  templateUrl: './pista-details.component.html',
  styleUrl: './pista-details.component.css'
})
export class PistaDetailsComponent implements OnInit{
  dropdownData!: any[];
  selectedItem: any;
  
  @Input() viewMode = false;

  @Input() currentPista: Pista = {
    nome: '',
    lat: '',
    long:'',
  };
  
  message = '';

  constructor(
    private pistaService: PistaService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getPista(this.route.snapshot.params["id"]);
    }
  }

  getPista(id: number): void {
    debugger;
      this.pistaService.get(id)
      .subscribe({
        next: (data) => {
          this.currentPista = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateProduto(): void {
    debugger;
    this.message = '';

    this.pistaService.update(this.currentPista.id,this.currentPista)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/pista";
        },
        error: (e) => console.error(e)
      });
  }

  deleteProduto(): void {
    debugger;
    this.pistaService.delete(this.currentPista.id)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/pista";
        },
        error: (e) => console.error(e)
      });
  }
}

import { Component, Input, OnInit } from '@angular/core';
import { Produto } from '../../../models/produto/produto.model';
import { ProdutoService } from '../../../services/produto/produto.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-produto-details',
  templateUrl: './produto-details.component.html',
  styleUrl: './produto-details.component.css'
})
export class ProdutoDetailsComponent implements OnInit{

  dropdownData!: any[];
  selectedItem: any;
  
  @Input() viewMode = false;

  @Input() currentProduto: Produto = {
    nome: '',
  };
  
  message = '';

  constructor(
    private produtoService: ProdutoService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getProduto(this.route.snapshot.params["id"]);
      this.produtoService.getDropdownData().subscribe(data => {
        this.dropdownData = data;
      });
    }
  }

  getProduto(id: number): void {
    debugger;
      this.produtoService.get(id)
      .subscribe({
        next: (data) => {
          this.currentProduto = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateProduto(): void {
    debugger;
    this.message = '';

    this.produtoService.update(this.currentProduto.id,this.currentProduto)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/produto";
        },
        error: (e) => console.error(e)
      });
  }

  deleteProduto(): void {
    debugger;
    this.produtoService.delete(this.currentProduto.id)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/produto";
        },
        error: (e) => console.error(e)
      });
  }
}

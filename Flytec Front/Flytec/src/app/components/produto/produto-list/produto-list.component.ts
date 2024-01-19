import { Component, OnInit } from '@angular/core';
import { Produto } from '../../../models/produto/produto.model';
import { ProdutoService } from '../../../services/produto/produto.service';

@Component({
  selector: 'app-produto-list',
  templateUrl: './produto-list.component.html',
  styleUrl: './produto-list.component.css'
})
export class ProdutoListComponent implements OnInit{
  produtos?: Produto[];
  currentProduto: Produto = {};
  currentIndex = -1;
  nome = '';

  constructor(private produtoService: ProdutoService) { }

  ngOnInit(): void {
    this.retrieveProdutos();
  }

  retrieveProdutos(): void {
    this.produtoService.getAll()
      .subscribe({
        next: (data) => {
          this.produtos = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrieveProdutos();
    this.currentProduto = {};
    this.currentIndex = -1;
  }

  setActiveProduto(produto: Produto, index: number): void {
    debugger;
    this.currentProduto = produto;
    this.currentIndex = index;
  }

  // removeAllTutorials(): void {
  //   this.tutorialService.deleteAll()
  //     .subscribe({
  //       next: (res) => {
  //         console.log(res);
  //         this.refreshList();
  //       },
  //       error: (e) => console.error(e)
  //     });
  // }

  // searchTitle(): void {
  //   this.currentTutorial = {};
  //   this.currentIndex = -1;

  //   this.tutorialService.findByTitle(this.nome)
  //     .subscribe({
  //       next: (data) => {
  //         this.tutorials = data;
  //         console.log(data);
  //       },
  //       error: (e) => console.error(e)
  //     });
  // }
}

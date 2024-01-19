import { Component } from '@angular/core';
import { Aplicacao } from '../../../models/aplicacao/aplicacao.model';
import { AplicacaoService } from '../../../services/aplicacao/aplicacao.service';

@Component({
  selector: 'app-aplicacao-list',
  templateUrl: './aplicacao-list.component.html',
  styleUrl: './aplicacao-list.component.css'
})
export class AplicacaoListComponent {
  aplicacoes?: Aplicacao[];
  currentAplicacao: Aplicacao = {};
  currentIndex = -1;
  nome = '';

  constructor(private aplicacaoService: AplicacaoService) { }

  ngOnInit(): void {
    this.retrieveAplicacao();
  }

  retrieveAplicacao(): void {
    this.aplicacaoService.getAll()
      .subscribe({
        next: (data) => {
          this.aplicacoes = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrieveAplicacao();
    this.currentAplicacao = {};
    this.currentIndex = -1;
  }

  setActiveAplicacao(aplicacao: Aplicacao, index: number): void {
    debugger;
    this.currentAplicacao = aplicacao;
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

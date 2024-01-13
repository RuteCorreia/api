import { Component } from '@angular/core';
import { Adjuvante } from '../../../models/adjuvante/adjuvante.model';
import { AdjuvanteService } from '../../../services/adjuvante/adjuvante.service';

@Component({
  selector: 'app-adjuvante-list',
  templateUrl: './adjuvante-list.component.html',
  styleUrl: './adjuvante-list.component.css'
})
export class AdjuvanteListComponent {
  adjuvantes?: Adjuvante[];
  currentAdjuvante: Adjuvante = {};
  currentIndex = -1;
  nome = '';

  constructor(private adjuvanteService: AdjuvanteService) { }

  ngOnInit(): void {
    this.retrieveAdjuvantes();
  }

  retrieveAdjuvantes(): void {
    this.adjuvanteService.getAll()
      .subscribe({
        next: (data) => {
          this.adjuvantes = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrieveAdjuvantes();
    this.currentAdjuvante = {};
    this.currentIndex = -1;
  }

  setActiveAdjuvante(adjuvante: Adjuvante, index: number): void {
    debugger;
    this.currentAdjuvante = adjuvante;
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

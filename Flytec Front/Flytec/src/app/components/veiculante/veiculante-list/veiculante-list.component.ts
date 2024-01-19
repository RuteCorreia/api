import { Component } from '@angular/core';
import { Veiculante } from '../../../models/veiculante/veiculante.model';
import { VeiculanteService } from '../../../services/veiculante/veiculante.service';

@Component({
  selector: 'app-veiculante-list',
  templateUrl: './veiculante-list.component.html',
  styleUrl: './veiculante-list.component.css'
})
export class VeiculanteListComponent {
  veiculantes?: Veiculante[];
  currentVeiculante: Veiculante = {};
  currentIndex = -1;
  nome = '';

  constructor(private veiculanteService: VeiculanteService) { }

  ngOnInit(): void {
    this.retrieveVeiculantes();
  }

  retrieveVeiculantes(): void {
    this.veiculanteService.getAll()
      .subscribe({
        next: (data) => {
          this.veiculantes = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrieveVeiculantes();
    this.currentVeiculante = {};
    this.currentIndex = -1;
  }

  setActiveVeiculante(veiculante: Veiculante, index: number): void {
    debugger;
    this.currentVeiculante = veiculante;
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

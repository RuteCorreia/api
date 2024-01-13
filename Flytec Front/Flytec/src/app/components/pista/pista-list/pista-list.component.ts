import { Component } from '@angular/core';
import { Pista } from '../../../models/pista/pista.model';
import { PistaService } from '../../../services/pista/pista.service';

@Component({
  selector: 'app-pista-list',
  templateUrl: './pista-list.component.html',
  styleUrl: './pista-list.component.css'
})
export class PistaListComponent {
  pistas?: Pista[];
  currentPista: Pista = {};
  currentIndex = -1;
  nome = '';

  constructor(private pistaService: PistaService) { }

  ngOnInit(): void {
    this.retrievePista();
  }

  retrievePista(): void {
    this.pistaService.getAll()
      .subscribe({
        next: (data) => {
          this.pistas = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrievePista();
    this.currentPista = {};
    this.currentIndex = -1;
  }

  setActivePista(pista: Pista, index: number): void {
    debugger;
    this.currentPista = pista;
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

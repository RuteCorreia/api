import { Component } from '@angular/core';
import { Cultura } from '../../../models/cultura/cultura.model';
import { CulturaService } from '../../../services/cultura/cultura.service';

@Component({
  selector: 'app-cultura-list',
  templateUrl: './cultura-list.component.html',
  styleUrl: './cultura-list.component.css'
})
export class CulturaListComponent {
  culturas?: Cultura[];
  currentCultura: Cultura = {};
  currentIndex = -1;
  nome = '';

  constructor(private culturaService: CulturaService) { }

  ngOnInit(): void {
    this.retrieveCultura();
  }

  retrieveCultura(): void {
    this.culturaService.getAll()
      .subscribe({
        next: (data) => {
          this.culturas = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrieveCultura();
    this.currentCultura = {};
    this.currentIndex = -1;
  }

  setActiveCultura(cultura: Cultura, index: number): void {
    debugger;
    this.currentCultura = cultura;
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

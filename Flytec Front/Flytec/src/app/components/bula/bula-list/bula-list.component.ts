import { Component } from '@angular/core';
import { Bula } from '../../../models/bula/bula.model';
import { BulaService } from '../../../services/bula/bula.service';

@Component({
  selector: 'app-bula-list',
  templateUrl: './bula-list.component.html',
  styleUrl: './bula-list.component.css'
})
export class BulaListComponent {
  bulas?: Bula[];
  currentBula: Bula = {};
  currentIndex = -1;
  nome = '';

  constructor(private bulaService: BulaService) { }

  ngOnInit(): void {
    this.retrieveBula();
  }

  retrieveBula(): void {
    this.bulaService.getAll()
      .subscribe({
        next: (data) => {
          this.bulas = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrieveBula();
    this.currentBula = {};
    this.currentIndex = -1;
  }

  setActiveBula(bula: Bula, index: number): void {
    debugger;
    this.currentBula = bula;
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

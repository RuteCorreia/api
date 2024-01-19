import { Component } from '@angular/core';
import { AlvoBiologico } from '../../../models/alvo-biologico/alvo-biologico.model';
import { AlvoBiologicoService } from '../../../services/alvo-biologico/alvo-biologico.service';

@Component({
  selector: 'app-alvo-biologico-list',
  templateUrl: './alvo-biologico-list.component.html',
  styleUrl: './alvo-biologico-list.component.css'
})
export class AlvoBiologicoListComponent {
  alvosBiologicos?: AlvoBiologico[];
  currentAlvoBiologico: AlvoBiologico = {};
  currentIndex = -1;
  nome = '';

  constructor(private alvoBiologicoService: AlvoBiologicoService) { }

  ngOnInit(): void {
    this.retrieveAlvoBiologico();
  }

  retrieveAlvoBiologico(): void {
    this.alvoBiologicoService.getAll()
      .subscribe({
        next: (data) => {
          this.alvosBiologicos = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrieveAlvoBiologico();
    this.currentAlvoBiologico = {};
    this.currentIndex = -1;
  }

  setActiveAlvoBiologico(alvoBiologico: AlvoBiologico, index: number): void {
    debugger;
    this.currentAlvoBiologico = alvoBiologico;
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

import { Component } from '@angular/core';
import { Aeronave } from '../../../models/aeronave/aeronave.model';
import { AeronaveService } from '../../../services/aeronave/aeronave.service';

@Component({
  selector: 'app-aeronave-list',
  templateUrl: './aeronave-list.component.html',
  styleUrls: ['./aeronave-list.component.css', '../../../../assets/css/generalConfig.css']
})
export class AeronaveListComponent {
  aeronaves?: Aeronave[];
  currentAeronave: Aeronave = {};
  currentIndex = -1;
  nome = '';

  constructor(private aeronaveService: AeronaveService) { }

  ngOnInit(): void {
    this.retrieveAeronave();
  }

  retrieveAeronave(): void {
    this.aeronaveService.getAll()
      .subscribe({
        next: (data) => {
          this.aeronaves = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrieveAeronave();
    this.currentAeronave = {};
    this.currentIndex = -1;
  }

  setActiveAeronave(aeronave: Aeronave, index: number): void {
    debugger;
    this.currentAeronave = aeronave;
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

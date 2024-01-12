import { Component } from '@angular/core';
import { Equipamento } from '../../../models/equipamento/equipamento.model';
import { EquipamentoService } from '../../../services/equipamento/equipamento.service';

@Component({
  selector: 'app-equipamento-list',
  templateUrl: './equipamento-list.component.html',
  styleUrl: './equipamento-list.component.css'
})
export class EquipamentoListComponent {

  equipamentos?: Equipamento[];
  currentEquipamento: Equipamento = {};
  currentIndex = -1;
  nome = '';

  constructor(private equipamentoService: EquipamentoService) { }

  ngOnInit(): void {
    this.retrieveEquipamento();
  }

  retrieveEquipamento(): void {
    this.equipamentoService.getAll()
      .subscribe({
        next: (data) => {
          this.equipamentos = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrieveEquipamento();
    this.currentEquipamento = {};
    this.currentIndex = -1;
  }

  setActiveEquipamento(equipamento: Equipamento, index: number): void {
    debugger;
    this.currentEquipamento = equipamento;
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

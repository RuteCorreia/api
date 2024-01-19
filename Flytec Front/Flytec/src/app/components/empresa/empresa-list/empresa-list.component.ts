import { Component } from '@angular/core';
import { Empresa } from '../../../models/empresa/empresa.model';
import { EmpresaService } from '../../../services/empresa/empresa.service';

@Component({
  selector: 'app-empresa-list',
  templateUrl: './empresa-list.component.html',
  styleUrl: './empresa-list.component.css'
})
export class EmpresaListComponent {
  empresas?: Empresa[];
  currentEmpresas: Empresa = {};
  currentIndex = -1;
  nome = '';

  constructor(private empresaService: EmpresaService) { }

  ngOnInit(): void {
    this.retrieveEmpresa();
  }

  retrieveEmpresa(): void {
    this.empresaService.getAll()
      .subscribe({
        next: (data) => {
          this.empresas = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  refreshList(): void {
    this.retrieveEmpresa();
    this.currentEmpresas = {};
    this.currentIndex = -1;
  }

  setActiveEmpresa(empresa: Empresa, index: number): void {
    debugger;
    this.currentEmpresas = empresa;
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

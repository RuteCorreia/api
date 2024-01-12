import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Aeronave } from '../../../models/aeronave/aeronave.model';
import { AeronaveService } from '../../../services/aeronave/aeronave.service';
import { Equipamento } from '../../../models/equipamento/equipamento.model';
import { EquipamentoService } from '../../../services/equipamento/equipamento.service';

@Component({
  selector: 'app-equipamento-details',
  templateUrl: './equipamento-details.component.html',
  styleUrl: './equipamento-details.component.css'
})
export class EquipamentoDetailsComponent implements OnInit{
  dropdownData!: any[];
  selectedItem: any;

  @Input() viewMode = false;

  @Input() currentEquipamento: Equipamento = {
    id: 0,
    nome: '',
  };
  
  message = '';

  constructor(
    private equipamentoService: EquipamentoService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getEquipamento(this.route.snapshot.params["id"]);
      this.equipamentoService.getDropdownData().subscribe(data => {
        this.dropdownData = data;
        debugger
      });
    }
  }

  getEquipamento(id: number): void {
    debugger;
      this.equipamentoService.get(id)
      .subscribe({
        next: (data) => {
          this.currentEquipamento = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateEquipamento(): void {
    debugger;
    this.message = '';

    this.equipamentoService.update(this.currentEquipamento.id,this.currentEquipamento)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/equipamento";
        },
        error: (e) => console.error(e)
      });
  }

  deleteEquipamento(): void {
    debugger;
    this.equipamentoService.delete(this.currentEquipamento.id)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/equipamento";
        },
        error: (e) => console.error(e)
      });
  }
}

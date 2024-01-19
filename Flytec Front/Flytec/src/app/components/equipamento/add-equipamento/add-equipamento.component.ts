import { Component } from '@angular/core';
import { Equipamento } from '../../../models/equipamento/equipamento.model';
import { EquipamentoService } from '../../../services/equipamento/equipamento.service';

@Component({
  selector: 'app-add-equipamento',
  templateUrl: './add-equipamento.component.html',
  styleUrl: './add-equipamento.component.css'
})
export class AddEquipamentoComponent {

voltar() {
window.location.href = "/equipamento";
}

  dropdownData!: any[];
  selectedItem: any;

  form: Equipamento = {
    id: 0,
    nome: ''
  };
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';

  constructor(private equipamentoService: EquipamentoService) { }
  
  onSubmit(): void {
    debugger;
    this.equipamentoService.create(this.form).subscribe({
      next: data => {
        console.log(data);
        this.isSuccessful = true;
        this.isSignUpFailed = false;
        window.location.href = "/equipamento";

      },
      error: err => {
        debugger;
        this.errorMessage = err;
        this.isSignUpFailed = true;
      }
    });
  }
}

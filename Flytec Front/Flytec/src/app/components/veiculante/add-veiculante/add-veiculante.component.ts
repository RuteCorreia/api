import { Component } from '@angular/core';
import { Veiculante } from '../../../models/veiculante/veiculante.model';
import { VeiculanteService } from '../../../services/veiculante/veiculante.service';

@Component({
  selector: 'app-add-veiculante',
  templateUrl: './add-veiculante.component.html',
  styleUrl: './add-veiculante.component.css'
})
export class AddVeiculanteComponent {
  voltar() {
    window.location.href = "/veiculante";
    }
    
      dropdownData!: any[];
      selectedItem: any;
    
      form: Veiculante = {
        idVeiculante: 0,
        nome: '',
      };
      isSuccessful = false;
      isSignUpFailed = false;
      errorMessage = '';
    
      constructor(private veiculanteService: VeiculanteService) { }
    
      onSubmit(): void {
        debugger;
        this.veiculanteService.create(this.form).subscribe({
          next: data => {
            console.log(data);
            this.isSuccessful = true;
            this.isSignUpFailed = false;
            window.location.href = "/veiculante";
    
          },
          error: err => {
            debugger;
            this.errorMessage = err;
            this.isSignUpFailed = true;
          }
        });
      }
}

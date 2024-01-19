import { Component } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { Adjuvante } from '../../models/adjuvante/adjuvante.model';
import { AdjuvanteService } from '../../services/adjuvante/adjuvante.service';

@Component({
  selector: 'app-adjuvante',
  templateUrl: './adjuvante.component.html',
  styleUrl: './adjuvante.component.css'
})
export class AdjuvanteComponent {
    
  voltar() {
    window.location.href = "/alvoBiologico";
    }
    
      dropdownData!: any[];
      selectedItem: any;
    
      form: Adjuvante = {
        id: 0,
        nome: '',
      };
      isSuccessful = false;
      isSignUpFailed = false;
      errorMessage = '';
    
      constructor(private adjuvanteService: AdjuvanteService) { }
    
      onSubmit(): void {
        debugger;
        this.adjuvanteService.create(this.form).subscribe({
          next: data => {
            console.log(data);
            this.isSuccessful = true;
            this.isSignUpFailed = false;
            window.location.href = "/adjuvante";
    
          },
          error: err => {
            debugger;
            this.errorMessage = err;
            this.isSignUpFailed = true;
          }
        });
      }
}

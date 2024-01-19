import { Component } from '@angular/core';
import { Cultura } from '../../../models/cultura/cultura.model';
import { CulturaService } from '../../../services/cultura/cultura.service';

@Component({
  selector: 'app-add-cultura',
  templateUrl: './add-cultura.component.html',
  styleUrl: './add-cultura.component.css'
})
export class AddCulturaComponent {

  voltar() {
    window.location.href = "/cultura";
    }
    
      dropdownData!: any[];
      selectedItem: any;
    
      form: Cultura = {
        idCultura: 0,
        nome: '',
        alvoBiologico: 'SemDado',
      };
      isSuccessful = false;
      isSignUpFailed = false;
      errorMessage = '';
    
      constructor(private culturaService: CulturaService) { }

      onSubmit(): void {
        debugger;
        this.culturaService.create(this.form).subscribe({
          next: data => {
            console.log(data);
            this.isSuccessful = true;
            this.isSignUpFailed = false;
            window.location.href = "/cultura";
    
          },
          error: err => {
            debugger;
            this.errorMessage = err;
            this.isSignUpFailed = true;
          }
        });
      }
}

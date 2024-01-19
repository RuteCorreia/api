import { Component, OnInit } from '@angular/core';
import { Pista } from '../../../models/pista/pista.model';
import { PistaService } from '../../../services/pista/pista.service';

@Component({
  selector: 'app-add-pista',
  templateUrl: './add-pista.component.html',
  styleUrl: './add-pista.component.css'
})
export class AddPistaComponent implements OnInit{

  voltar() {
    window.location.href = "/pista";
    }
    
      dropdownData!: any[];
      selectedItem: any;
    
      form: Pista = {
        id: 0,
        nome: '',
        lat: '',
        long: '',
      };
      isSuccessful = false;
      isSignUpFailed = false;
      errorMessage = '';
    
      constructor(private pistaService: PistaService) { }
      
      ngOnInit(): void {
      }
      onSubmit(): void {
        debugger;
        this.pistaService.create(this.form).subscribe({
          next: data => {
            console.log(data);
            this.isSuccessful = true;
            this.isSignUpFailed = false;
            window.location.href = "/pista";
    
          },
          error: err => {
            debugger;
            this.errorMessage = err;
            this.isSignUpFailed = true;
          }
        });
      }
}

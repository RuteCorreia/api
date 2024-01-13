import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';
import { AlvoBiologico } from '../../../models/alvo-biologico/alvo-biologico.model';
import { AlvoBiologicoService } from '../../../services/alvo-biologico/alvo-biologico.service';

@Component({
  selector: 'app-add-alvo-biologico',
  templateUrl: './add-alvo-biologico.component.html',
  styleUrl: './add-alvo-biologico.component.css'
})
export class AddAlvoBiologicoComponent implements OnInit{
  
  voltar() {
    window.location.href = "/alvoBiologico";
    }
    
      dropdownData!: any[];
      selectedItem: any;
    
      form: AlvoBiologico = {
        id: 0,
        idProduto: 0,
        nome: '',
        doseProdutoPorHectare: '',
      };
      isSuccessful = false;
      isSignUpFailed = false;
      errorMessage = '';
    
      constructor(private alvoBiologicoService: AlvoBiologicoService) { }
      
      ngOnInit(): void {
        this.alvoBiologicoService.getDropdownData().subscribe(data => {
          this.dropdownData = data;
          debugger
        });
      }
      onSubmit(): void {
        debugger;
        this.alvoBiologicoService.create(this.form).subscribe({
          next: data => {
            console.log(data);
            this.isSuccessful = true;
            this.isSignUpFailed = false;
            window.location.href = "/alvoBiologico";
    
          },
          error: err => {
            debugger;
            this.errorMessage = err;
            this.isSignUpFailed = true;
          }
        });
      }
}

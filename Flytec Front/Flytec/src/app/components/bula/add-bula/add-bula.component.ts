import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';
import { Bula } from '../../../models/bula/bula.model';
import { BulaService } from '../../../services/bula/bula.service';

@Component({
  selector: 'app-add-bula',
  templateUrl: './add-bula.component.html',
  styleUrl: './add-bula.component.css'
})
export class AddBulaComponent {
  voltar() {
    window.location.href = "/bula";
    }
    
      dropdownDataAlvoBiologico!: any[];
      dropdownDataCultura!: any[];

      selectedItem: any;
    
      form: Bula = {
        idBula: 0,
        nomeProduto: '',
        idCultura: 0,
        idClassificacaoToxicologica: 0,
        classe: '',
        tipoDeFormulacao: '',
        idAlvoBiologico: 0,
        doseProdutoComercial: 0,
        adjuvante: '',
        idTipoDeServico: 0,
      };
      isSuccessful = false;
      isSignUpFailed = false;
      errorMessage = '';
    
      constructor(private bulaService: BulaService) { }
      
      ngOnInit(): void {
        this.bulaService.getDropdownAlvoBiologico().subscribe(data => {
          this.dropdownDataAlvoBiologico = data;
        });
        this.bulaService.getDropdownCultura().subscribe(data => {
          this.dropdownDataCultura = data;
        });
      }
      onSubmit(): void {
        debugger;
        this.bulaService.create(this.form).subscribe({
          next: data => {
            console.log(data);
            this.isSuccessful = true;
            this.isSignUpFailed = false;
            window.location.href = "/bula";
    
          },
          error: err => {
            debugger;
            this.errorMessage = err;
            this.isSignUpFailed = true;
          }
        });
      }
}

import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';
import { Aplicacao } from '../../../models/aplicacao/aplicacao.model';
import { AplicacaoService } from '../../../services/aplicacao/aplicacao.service';

@Component({
  selector: 'app-add-aplicacao',
  templateUrl: './add-aplicacao.component.html',
  styleUrl: './add-aplicacao.component.css'
})
export class AddAplicacaoComponent implements OnInit {

  voltar() {
    window.location.href = "/aplicacao";
    }
    
      dropdownDataEmpresa!: any[];
      dropdownDataPiloto!: any[];
      dropdownDataExecutor!: any[];
      dropdownDataCliente!: any[];
      dropdownDataCultura!: any[];

      selectedItem: any;
    
      form: Aplicacao = {
        id: 0,
        idEmpresa: 0,
        statusEnvio: '',
        idPiloto: 0,
        idExecutor: 0,
        idCliente: 0,
        idCultura: 0,
      };
      isSuccessful = false;
      isSignUpFailed = false;
      errorMessage = '';
    
      constructor(private aplicacaoService: AplicacaoService) { }
      
      ngOnInit(): void {
        this.aplicacaoService.getDropdownCliente().subscribe(data => {
          this.dropdownDataCliente = data;
        });
        this.aplicacaoService.getDropdownCultura().subscribe(data => {
          this.dropdownDataCultura = data;
        });
        this.aplicacaoService.getDropdownEmpresa().subscribe(data => {
          this.dropdownDataEmpresa = data;
        });
        this.aplicacaoService.getDropdownExecutor().subscribe(data => {
          this.dropdownDataExecutor = data;
        });
        this.aplicacaoService.getDropdownPiloto().subscribe(data => {
          this.dropdownDataPiloto = data;
        });
      }
      onSubmit(): void {
        debugger;
        this.aplicacaoService.create(this.form).subscribe({
          next: data => {
            console.log(data);
            this.isSuccessful = true;
            this.isSignUpFailed = false;
            window.location.href = "/aplicacao";
    
          },
          error: err => {
            debugger;
            this.errorMessage = err;
            this.isSignUpFailed = true;
          }
        });
      }
}

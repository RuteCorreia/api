import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-bula',
  templateUrl: './add-bula.component.html',
  styleUrl: './add-bula.component.css'
})
export class AddBulaComponent {
  form: any = {
    nomeProduto: null,
    idCultura: null,
    idClassificacaoToxicologica: null,
    classe: null,
    tipoDeFormulacao: null,
    idAlvoBiologico: null,
    doseProdutoComercial: null,
    adjuvante: null,
    idTipoDeServico: null,
  };
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';

  constructor(private authService: AuthService) { }

  // onSubmit(): void {
  //   const { nome } = this.form;

  //   this.authService.register(nome).subscribe({
  //     next: data => {
  //       console.log(data);
  //       this.isSuccessful = true;
  //       this.isSignUpFailed = false;
  //     },
  //     error: err => {
  //       this.errorMessage = err.error;
  //       this.isSignUpFailed = true;
  //     }
  //   });
  // }
}

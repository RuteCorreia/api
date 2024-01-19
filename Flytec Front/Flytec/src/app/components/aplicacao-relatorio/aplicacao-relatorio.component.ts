import { Component } from '@angular/core';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-aplicacao-relatorio',
  templateUrl: './aplicacao-relatorio.component.html',
  styleUrl: './aplicacao-relatorio.component.css'
})
export class AplicacaoRelatorioComponent {
  form: any = {
    idAplicacao: null,
    idPista: null,
    dosagem: null,
    kG_LT: null,
    volumeAplicacao: null,
    totalAreaAplicada: null,
    alteracoes_Observacoes: null,
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

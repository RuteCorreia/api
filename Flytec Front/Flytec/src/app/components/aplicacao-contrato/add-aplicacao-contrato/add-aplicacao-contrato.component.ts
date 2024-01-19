import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-aplicacao-contrato',
  templateUrl: './add-aplicacao-contrato.component.html',
  styleUrl: './add-aplicacao-contrato.component.css'
})
export class AddAplicacaoContratoComponent {
  form: any = {
    idAplicacao: null,
    idUF: null,
    idCidade: null,
    nomeCliente: null,
    cpfCliente: null,
    assinatura: null,
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

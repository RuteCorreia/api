import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-aplicacao',
  templateUrl: './add-aplicacao.component.html',
  styleUrl: './add-aplicacao.component.css'
})
export class AddAplicacaoComponent {
  form: any = {
    idEmpresa: null,
    statusEnvio: null,
    idPiloto: null,
    idExecutor: null,
    idCliente: null,
    idCultura: null,

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

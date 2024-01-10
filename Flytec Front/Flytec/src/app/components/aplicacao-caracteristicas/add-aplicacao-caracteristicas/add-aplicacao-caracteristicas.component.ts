import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-aplicacao-caracteristicas',
  templateUrl: './add-aplicacao-caracteristicas.component.html',
  styleUrl: './add-aplicacao-caracteristicas.component.css'
})
export class AddAplicacaoCaracteristicasComponent {
  form: any = {
    idAplicacao: null,
    idProduto: null,
    idAdjuvante: null,
    tipoDeServico: null,
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

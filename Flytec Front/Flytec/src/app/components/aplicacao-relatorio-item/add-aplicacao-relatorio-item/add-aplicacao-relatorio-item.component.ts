import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-aplicacao-relatorio-item',
  templateUrl: './add-aplicacao-relatorio-item.component.html',
  styleUrl: './add-aplicacao-relatorio-item.component.css'
})
export class AddAplicacaoRelatorioItemComponent {
  form: any = {
    idAplicacaoRelatorio: null,
    horaInicio: null,
    horimetroInicial: null,
    horaTermino: null,
    horimetroTermino: null,
    temperaturaInicial: null,
    temperaturaFinal: null,
    urInicial: null,
    urFinal: null,
    ventoInicial: null,
    ventoFinal: null,
    imagemDadosClimaticos: null,
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

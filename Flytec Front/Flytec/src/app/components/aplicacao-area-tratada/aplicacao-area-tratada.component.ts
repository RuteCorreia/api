import { Component } from '@angular/core';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-aplicacao-area-tratada',
  templateUrl: './aplicacao-area-tratada.component.html',
  styleUrl: './aplicacao-area-tratada.component.css'
})
export class AplicacaoAreaTratadaComponent {
  form: any = {
    idAplicacao: null,
    idEstado: null,
    idCidade: null,
    localizacao: null,
    extensao: null,
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

import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-aplicacao-croqui-importacao',
  templateUrl: './add-aplicacao-croqui-importacao.component.html',
  styleUrl: './add-aplicacao-croqui-importacao.component.css'
})
export class AddAplicacaoCroquiImportacaoComponent {
  form: any = {
    idAplicacaoCroqui: null,
    arquivo: null,
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

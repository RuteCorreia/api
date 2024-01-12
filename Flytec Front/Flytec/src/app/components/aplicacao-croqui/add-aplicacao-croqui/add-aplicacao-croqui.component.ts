import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-aplicacao-croqui',
  templateUrl: './add-aplicacao-croqui.component.html',
  styleUrl: './add-aplicacao-croqui.component.css'
})
export class AddAplicacaoCroquiComponent {
  form: any = {
    idAplicacao: null,
    desenho: null,
    idMapa: null,
    latitude: null,
    longitude: null,
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

import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-aeronave',
  templateUrl: './add-aeronave.component.html',
  styleUrl: './add-aeronave.component.css'
})
export class AddAeronaveComponent {
  form: any = {
    idEmpresa: null,
    prefixo: null,
    combustivel: null,
    capacidadeDeCarga: null,
    horimetro: null,

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

import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-combate-incendio-decolagem-pouso',
  templateUrl: './add-combate-incendio-decolagem-pouso.component.html',
  styleUrl: './add-combate-incendio-decolagem-pouso.component.css'
})
export class AddCombateIncendioDecolagemPousoComponent {
  form: any = {
    idCombateIncendio: null,
    decolagemHorario: null,
    decolagemHorimetro: null,
    pousoHorario: null,
    pousoHorimetro: null,
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

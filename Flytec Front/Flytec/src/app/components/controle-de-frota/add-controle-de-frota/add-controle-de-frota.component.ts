import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-controle-de-frota',
  templateUrl: './add-controle-de-frota.component.html',
  styleUrl: './add-controle-de-frota.component.css'
})
export class AddControleDeFrotaComponent {
  form: any = {
    observacao: null,
    data: null,
    idFrota: null,
    idAeronave: null,
    kmInicial: null,
    localInicial: null,
    localizacaoPistaLat: null,
    localizacaoPistaLon: null,
    kmFinal: null,
    horimetroInicial: null,
    horimetroFinal: null,
    combustivel: null,
    qtdeCombustivel: null,
    qtdeHectare: null,
    idPiloto: null,
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

import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-aplicacao-recomendacoes-tecnicas',
  templateUrl: './add-aplicacao-recomendacoes-tecnicas.component.html',
  styleUrl: './add-aplicacao-recomendacoes-tecnicas.component.css'
})
export class AddAplicacaoRecomendacoesTecnicasComponent {
  form: any = {
    idAplicacao: null,
    idVeiculante: null,
    qtdeVeiculante: null,
    larguraFaixa: null,
    volumeAplicacao: null,
    idAeronave: null,
    idAlturaVoo: null,
    alturaVooCustom: null,
    temperatura: null,
    urDoAR: null,
    velocidadeVento: null,
    idTipoDeProduto: null,
    idEquipamento: null,
    angulo: null,
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

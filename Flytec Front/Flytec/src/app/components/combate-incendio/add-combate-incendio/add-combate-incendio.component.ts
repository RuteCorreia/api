import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-combate-incendio',
  templateUrl: './add-combate-incendio.component.html',
  styleUrl: './add-combate-incendio.component.css'
})
export class AddCombateIncendioComponent {
  form: any = {
    idEmpresa: null,
    idExecutor: null,
    aviso: null,
    idAeronave: null,
    idPista: null,
    data: null,
    horaInicial: null,
    horimetroAviao: null,
    localIncendioLat: null,
    localIncendioLon: null,
    referencia: null,
    horarioFinalOperacao: null,
    horimetroFinalOperacao: null,
    totalAguaUtilizadaOperacao: null,
    coordenadorBaseOperacionalNome: null,
    coordenadorBaseOperacionalPosto: null,
    coordenadorBaseOperacionalRE: null,
    coordenadorBaseOperacionalAssinatura: null,
    comandanteOcorrenciaNome: null,
    comandanteOcorrenciaPosto: null,
    comandanteOcorrenciaRE: null,
    comandanteOcorrenciaAssinatura: null,
    responsavelOcorrenciaNome: null,
    responsavelOcorrenciaPosto: null,
    responsavelOcorrenciaRE: null,
    responsavelOcorrenciaAssinatura: null,
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

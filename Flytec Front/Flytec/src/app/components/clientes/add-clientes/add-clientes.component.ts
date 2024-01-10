import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-clientes',
  templateUrl: './add-clientes.component.html',
  styleUrl: './add-clientes.component.css'
})
export class AddClientesComponent {
  form: any = {
    nomeCliente: null,
    idTipoCliente: null,
    cpf: null,
    rg: null,
    cnpj: null,
    inscricaoEstadual: null,
    endereco: null,
    telefone1: null,
    telefone2: null,
    email: null,
    senha: null,
    precificacao: null,
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

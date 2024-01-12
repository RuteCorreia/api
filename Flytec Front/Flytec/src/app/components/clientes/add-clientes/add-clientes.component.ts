import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';
import { ClienteService } from '../../../services/cliente/cliente.service';
import { Cliente } from '../../../models/cliente/cliente.model';

@Component({
  selector: 'app-add-clientes',
  templateUrl: './add-clientes.component.html',
  styleUrl: './add-clientes.component.css'
})
export class AddClientesComponent  implements OnInit{

voltar() {
window.location.href = "/cliente";
}

  dropdownData!: any[];
  selectedItem: any;

  form: Cliente = {
    nomeCliente: '',
    idTipoCliente: 0,
    cpf: '',
    rg: '',
    cnpj: '',
    inscricaoEstadual: '',
    endereco: '',
    telefone1: '',
    telefone2: '',
    email: '',
    senha: '',
    precificacao: ''
  };
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';

  constructor(private clienteService: ClienteService) { }
  
  ngOnInit(): void {
    // this.clienteService.getDropdownData().subscribe(data => {
    //   this.dropdownData = data;
    // });
  }
  onSubmit(): void {
    debugger;
    this.clienteService.create(this.form).subscribe({
      next: data => {
        console.log(data);
        this.isSuccessful = true;
        this.isSignUpFailed = false;
        window.location.href = "/cliente";

      },
      error: err => {
        debugger;
        this.errorMessage = err;
        this.isSignUpFailed = true;
      }
    });
  }
}

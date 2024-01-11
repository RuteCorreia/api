import { Component, Input, OnInit } from '@angular/core';
import { Cliente } from '../../../models/cliente/cliente.model';
import { ClienteService } from '../../../services/cliente.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-clientes-details',
  templateUrl: './clientes-details.component.html',
  styleUrl: './clientes-details.component.css'
})
export class ClientesDetailsComponent implements OnInit {
  
  @Input() viewMode = false;

  @Input() currentCliente: Cliente = {
    nomeCliente: '',
    email: '',
  };
  
  message = '';

  constructor(
    private clienteService: ClienteService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getCliente(this.route.snapshot.params["id"]);
    }
  }

  getCliente(id: number): void {
    debugger;
      this.clienteService.get(id)
      .subscribe({
        next: (data) => {
          this.currentCliente = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateCliente(): void {
    debugger;
    this.message = '';

    this.clienteService.update(this.currentCliente.idCliente,this.currentCliente)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/cliente";
        },
        error: (e) => console.error(e)
      });
  }

  deleteCliente(): void {
    debugger;
    this.clienteService.delete(this.currentCliente.idCliente)
      .subscribe({
        next: (res) => {
          console.log(res);
          this.router.navigate(['/cliente']);
        },
        error: (e) => console.error(e)
      });
  }
}

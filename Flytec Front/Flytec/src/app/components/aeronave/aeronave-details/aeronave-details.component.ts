import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Cliente } from '../../../models/cliente/cliente.model';
import { ClienteService } from '../../../services/cliente/cliente.service';
import { Aeronave } from '../../../models/aeronave/aeronave.model';
import { AeronaveService } from '../../../services/aeronave/aeronave.service';

@Component({
  selector: 'app-aeronave-details',
  templateUrl: './aeronave-details.component.html',
  styleUrl: './aeronave-details.component.css'
})
export class AeronaveDetailsComponent implements OnInit {

  dropdownData!: any[];
  selectedItem: any;

  @Input() viewMode = false;

  @Input() currentAeronave: Aeronave = {
    prefixo: '',
    combustivel: '',
  };
  
  message = '';

  constructor(
    private aeronaveService: AeronaveService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      debugger;
      this.getAeronave(this.route.snapshot.params["id"]);
      this.aeronaveService.getDropdownData().subscribe(data => {
        this.dropdownData = data;
        debugger
      });
    }
  }

  getAeronave(id: number): void {
    debugger;
      this.aeronaveService.get(id)
      .subscribe({
        next: (data) => {
          this.currentAeronave = data;
          console.log(data);

        },
        error: (e) => {
        }
        
      });    
  }

  updateAeronave(): void {
    debugger;
    this.message = '';

    this.aeronaveService.update(this.currentAeronave.id,this.currentAeronave)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/aeronave";
        },
        error: (e) => console.error(e)
      });
  }

  deleteAeronave(): void {
    debugger;
    this.aeronaveService.delete(this.currentAeronave.id)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.href = "/aeronave";
        },
        error: (e) => console.error(e)
      });
  }
}

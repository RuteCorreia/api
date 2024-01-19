import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';
import { Aeronave } from '../../../models/aeronave/aeronave.model';
import { AeronaveService } from '../../../services/aeronave/aeronave.service';

@Component({
  selector: 'app-add-aeronave',
  templateUrl: './add-aeronave.component.html',
  styleUrl: './add-aeronave.component.css'
})
export class AddAeronaveComponent {

  voltar() {
window.location.href = "/produto";
}

  dropdownData!: any[];
  selectedItem: any;

  form: Aeronave = {
    id: 0,
    idEmpresa: 0,
    prefixo: '',
    combustivel: '',
    capacidadeDeCarga: 0,
    horimetro: '',
  };
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';

  constructor(private aeronaveService: AeronaveService) { }
  
  ngOnInit(): void {
    this.aeronaveService.getDropdownData().subscribe(data => {
      this.dropdownData = data;
      debugger
    });
  }
  onSubmit(): void {
    debugger;
    this.aeronaveService.create(this.form).subscribe({
      next: data => {
        console.log(data);
        this.isSuccessful = true;
        this.isSignUpFailed = false;
        window.location.href = "/aeronave";

      },
      error: err => {
        debugger;
        this.errorMessage = err;
        this.isSignUpFailed = true;
      }
    });
  }
}

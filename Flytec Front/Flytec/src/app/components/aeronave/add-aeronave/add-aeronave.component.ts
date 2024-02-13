import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';
import { Aeronave } from '../../../models/aeronave/aeronave.model';
import { AeronaveService } from '../../../services/aeronave/aeronave.service';

@Component({
  selector: 'app-add-aeronave',
  templateUrl: './add-aeronave.component.html',
  styleUrls: ['./add-aeronave.component.css', '../../../../assets/css/generalConfig.css']
})

export class AddAeronaveComponent {

  voltar() {
  	window.location.href = "/aeronave";
  }

  dropdownData!: any[];
  selectedItem: any;

  form: Aeronave = {
    id: 0,
    idEmpresa: 0,
    prefixo: '',
    modelo: '',
    serialNumber: '',
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

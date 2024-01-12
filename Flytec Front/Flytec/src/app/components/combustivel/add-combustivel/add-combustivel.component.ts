import { Component } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-add-combustivel',
  templateUrl: './add-combustivel.component.html',
  styleUrl: './add-combustivel.component.css'
})
export class AddCombustivelComponent {
  form: any = {
    nome: null,
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

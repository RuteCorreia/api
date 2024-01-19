import { Component } from '@angular/core';
import { Empresa } from '../../../models/empresa/empresa.model';
import { EmpresaService } from '../../../services/empresa/empresa.service';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-add-empresa',
  templateUrl: './add-empresa.component.html',
  styleUrl: './add-empresa.component.css'
})
export class AddEmpresaComponent {
  imageForm: FormGroup;
  imageData?: string | ArrayBuffer | null;


  voltar() {
    window.location.href = "/empresa";
    }
    
      dropdownData!: any[];
      selectedItem: any;
    
      form: Empresa = {
        idEmpresa: 0,
        nome: '',
        imagem:'',
        planoContratado:2,
      };
      isSuccessful = false;
      isSignUpFailed = false;
      errorMessage = '';
    
      constructor(private empresaService: EmpresaService, private fb: FormBuilder) { 
        this.imageForm = this.fb.group({
          image: [null]});
      }
    
      onSubmit(): void {
        debugger;
        const imageData = this.imageData;
        this.form.imagem = imageData;
        this.empresaService.create(this.form).subscribe({
          next: data => {
            console.log(data);
            this.isSuccessful = true;
            this.isSignUpFailed = false;
            window.location.href = "/empresa";
    
          },
          error: err => {
            debugger;
            this.errorMessage = err;
            this.isSignUpFailed = true;
          }
        });
      }
      onFileChange(event: any): void {
        const reader = new FileReader();
    
        if (event.target.files && event.target.files.length) {
          const [file] = event.target.files;
          reader.readAsDataURL(file);
    
          reader.onload = () => {
            this.imageData = reader.result;

          };
        }
      }
}

import { Component, OnInit } from '@angular/core';
import { Produto } from '../../../models/produto/produto.model';
import { ProdutoService } from '../../../services/produto/produto.service';

@Component({
  selector: 'app-add-produto',
  templateUrl: './add-produto.component.html',
  styleUrl: './add-produto.component.css'
})
export class AddProdutoComponent implements OnInit{

voltar() {
window.location.href = "/produto";
}

  dropdownData!: any[];
  selectedItem: any;

  form: Produto = {
    id: 0,
    idCultura: 0,
    nome: '',
    classificacaoToxicologica: '',
    classe: '',
    tipoDeFormulacao: '',
    tipoServico: ''
  };
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';

  constructor(private produtoService: ProdutoService) { }
  
  ngOnInit(): void {
    this.produtoService.getDropdownData().subscribe(data => {
      this.dropdownData = data;
      debugger
    });
  }
  onSubmit(): void {
    debugger;
    this.produtoService.create(this.form).subscribe({
      next: data => {
        console.log(data);
        this.isSuccessful = true;
        this.isSignUpFailed = false;
        window.location.href = "/produto";

      },
      error: err => {
        debugger;
        this.errorMessage = err;
        this.isSignUpFailed = true;
      }
    });
  }
}

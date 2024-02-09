import { Component } from '@angular/core';

@Component({
  selector: 'app-add-componente',
  templateUrl: './add-componente.component.html',
  styleUrl: './add-componente.component.css'
})
export class AddComponenteComponent {
  isSuccessful: boolean = false;

  ngOnInit(): void {
    
  }

  voltar(): void {
    window.location.href = '/componente';
  }
}

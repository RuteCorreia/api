import { Component } from '@angular/core';

@Component({
  selector: 'app-componente-list',
  templateUrl: './componente-list.component.html',
  styleUrls: ['./componente-list.component.css', '../../../../assets/css/generalConfig.css']
})

export class ComponenteListComponent {
  componentes!: any[];
  currentIndex: number = -1;
}

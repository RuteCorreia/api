import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
  template: `
    <div (click)="toggleNavbar()">Toggle Navbar</div>
    <div *ngIf="isNavbarVisible">Navbar Content</div>
  `,
})
export class NavbarComponent {
  @Output() toggle = new EventEmitter<boolean>();
  isNavbarVisible = true;

  toggleNavbar() {
    this.isNavbarVisible = !this.isNavbarVisible;
    this.toggle.emit(this.isNavbarVisible);
  }

  stopPropagation(event: Event): void {
    event.stopPropagation();
  }

  goToPage(pageRoute: String): void {
    window.location.href = `/${pageRoute}`; 
  }
}

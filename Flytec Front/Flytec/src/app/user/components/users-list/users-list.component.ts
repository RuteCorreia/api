import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../../user.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { Router, RouterLink } from '@angular/router';
import { take } from 'rxjs';
import { User } from '../../userModel';
import { FormBuilder } from '@angular/forms';
// *ngIf="users().length"

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule,RouterLink, MatIconModule],
  template: `
    <section class="container" >
      <div class="container__header">
        <span>Usuários</span>
      </div>
      <table mat-table [dataSource]="users()" class="mat-elevation-z8">
        <ng-container
          [matColumnDef]="column"
          *ngFor="let column of displayedColumns"
        >
          <th mat-header-cell *matHeaderCellDef>{{ column | titlecase }}</th>
          <td mat-cell *matCellDef="let element">{{ element[column] }}</td>
        </ng-container>
        <ng-container matColumnDef="action">
          <th mat-header-cell *matHeaderCellDef>Ação</th>
          <td mat-cell *matCellDef="let user">
            <button mat-icon-button color="accent"
            >
              <mat-icon>edit</mat-icon>
            </button>
            <button mat-icon-button color="accent"
            (click)="deleteTodo(user.id)"
>
              <mat-icon>delete</mat-icon>
            </button>
          </td>
        </ng-container>

        <tr mat-header-row *matHeaderRowDef="fullColumns"></tr>
        <tr mat-row *matRowDef="let row; columns: fullColumns"></tr>
      </table>
    </section>
  `,
  styles: [
    `
      th,
      td {
        text-align: center;
      }

      .container {
        padding: 2rem 10rem;
        gap: 2rem;

        display: flex;
        flex-direction: column;
        align-items: center;

        &__header {
          > span {
            font-size: 2rem;
            line-heith: 1rem;
          }
          width: 100%;
          display: flex;
          justify-content: space-between;
          align-items: center;
          text-align: center;
        }
      }
    `,
  ],
})
export class UsersListComponent {
  public displayedColumns = ['id', 'nome', 'email'];

  public fullColumns = ['id', 'nome', 'email', 'action'];

  public userService = inject(UserService);

  public users = this.userService.users;
  public router = inject(Router);

  deleteTodo(todoId: number | undefined) {
    if (!todoId) return;
    this.userService.deletePost(todoId).pipe(take(1)).subscribe();
  }

  // editTodo(todoId: User | undefined) {
  //   if (!todoId) return;
  //   this.userService.editTodo(todoId);
  // }


}
import { HttpClient } from '@angular/common/http';
import { Injectable, inject, runInInjectionContext, signal } from '@angular/core';
import { User } from './userModel';
import { toObservable, toSignal } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { Observable, switchMap, tap } from 'rxjs';
import { ResourceService } from '../resource/resource.service';
import { FormBuilder } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})

export class UserService extends ResourceService<User>{

  public userUrl = 'https://localhost:7221/api/v1/auth/users';
  public removeUrl = 'https://localhost:7221/api/v1/auth/removeUser';

  private users$ = this.http.get<User[]>(this.userUrl);

  public users = toSignal(this.users$, { initialValue: [] as User[] });

  public selectedUserId = signal(0);
  public router = inject(Router);
  public userTasks = signal<User[]>([]);

  deletePost(id: number): Observable<string> {
    return this.http
      .delete<string>(`https://localhost:7221/api/v1/auth/removeUser?userId=` + id)
      .pipe(tap(() => this.removeResource(id)));
  }

  private userTasks$ = toObservable(this.selectedUserId).pipe(
    switchMap((userId) =>
      this.http
        .get<User[]>(`${this.userUrl}?userId=${userId}`)
        .pipe(tap((tasks) => {
          this.userTasks.set(tasks);

        }))
    )
  );

  public setSelectedUserId(id: number): void {
    this.selectedUserId.set(id);
    this.router.navigateByUrl(`https://localhost:7221/api/v1/auth/removeUser/${id}`);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { User } from './userModel';
import { toSignal } from '@angular/core/rxjs-interop';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public http = inject(HttpClient);

  public userUrl = 'https://localhost:7221/api/v1/auth/users';
  public removeUrl = 'https://localhost:7221/api/v1/auth/removeUser';

  private users$ = this.http.get<User[]>(this.userUrl);

  public users = toSignal(this.users$, { initialValue: [] as User[] });

  public remove = this.http.delete(this.removeUrl);
}

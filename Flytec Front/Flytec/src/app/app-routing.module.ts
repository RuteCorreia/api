import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { BoardAdminComponent } from './board-admin/board-admin.component';
import { UsersListComponent } from './components/users-list/users-list.component';
import { UsersDetailsComponent } from './components/users-details/users-details.component';
import { DashboardComponent } from './dashboard/components/dashboard/dashboard.component';
const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'novoUsuario', component: RegisterComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'admin', component: BoardAdminComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'usuarios', component: UsersListComponent },
  { path: 'usuario/:id', component: UsersDetailsComponent },
  { path: 'dash', component: DashboardComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

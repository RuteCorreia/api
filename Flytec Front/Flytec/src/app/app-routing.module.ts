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
import { AdjuvanteComponent } from './components/adjuvante/adjuvante.component';
import { AddAeronaveComponent } from './components/aeronave/add-aeronave/add-aeronave.component';
import { AddAlturaVooComponent } from './components/altura-voo/add-altura-voo/add-altura-voo.component';
import { AddAlvoBiologicoComponent } from './components/alvo-biologico/add-alvo-biologico/add-alvo-biologico.component';
import { AddAplicacaoComponent } from './components/aplicacao/add-aplicacao/add-aplicacao.component';
import { AddAplicacaoAreaTratadaComponent } from './components/aplicacao-area-tratada/add-aplicacao-area-tratada/add-aplicacao-area-tratada.component';
import { AddAplicacaoCaracteristicasComponent } from './components/aplicacao-caracteristicas/add-aplicacao-caracteristicas/add-aplicacao-caracteristicas.component';
import { AddAplicacaoContratoComponent } from './components/aplicacao-contrato/add-aplicacao-contrato/add-aplicacao-contrato.component';
import { AddAplicacaoCroquiComponent } from './components/aplicacao-croqui/add-aplicacao-croqui/add-aplicacao-croqui.component';
import { AddAplicacaoCroquiImportacaoComponent } from './components/aplicacao-croqui-importacao/add-aplicacao-croqui-importacao/add-aplicacao-croqui-importacao.component';
import { AddAplicacaoLogComponent } from './components/aplicacao-log/add-aplicacao-log/add-aplicacao-log.component';
import { AddAplicacaoRecomendacoesTecnicasComponent } from './components/aplicacao-recomendacoes-tecnicas/add-aplicacao-recomendacoes-tecnicas/add-aplicacao-recomendacoes-tecnicas.component';
import { AddAplicacaoRelatorioComponent } from './components/aplicacao-relatorio/add-aplicacao-relatorio/add-aplicacao-relatorio.component';
import { AddAplicacaoRelatorioItemComponent } from './components/aplicacao-relatorio-item/add-aplicacao-relatorio-item/add-aplicacao-relatorio-item.component';
import { AddBulaComponent } from './components/bula/add-bula/add-bula.component';
import { AddCidadesComponent } from './components/cidades/add-cidades/add-cidades.component';
import { AddClientesComponent } from './components/clientes/add-clientes/add-clientes.component';
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
  { path: 'adjuvante', component: AdjuvanteComponent },
  { path: 'aeronave', component: AddAeronaveComponent },
  { path: 'alturaVoo', component: AddAlturaVooComponent },
  { path: 'alvoBiologico', component: AddAlvoBiologicoComponent },
  { path: 'aplicacao', component: AddAplicacaoComponent },
  { path: 'aplicacaoAreaTratada', component: AddAplicacaoAreaTratadaComponent },
  { path: 'aplicacaoCaracteristicas', component: AddAplicacaoCaracteristicasComponent },
  { path: 'aplicacaoContrato', component: AddAplicacaoContratoComponent },
  { path: 'aplicacaoCroqui', component: AddAplicacaoCroquiComponent },
  { path: 'aplicacaoCroquiImportacao', component: AddAplicacaoCroquiImportacaoComponent },
  { path: 'aplicacaoLog', component: AddAplicacaoLogComponent },
  { path: 'aplicacaoRecomendacoesTecnicas', component: AddAplicacaoRecomendacoesTecnicasComponent },
  { path: 'aplicacaoRelatorio', component: AddAplicacaoRelatorioComponent },
  { path: 'aplicacaoRelatorioItem', component: AddAplicacaoRelatorioItemComponent },
  { path: 'bula', component: AddBulaComponent },
  { path: 'cidade', component: AddCidadesComponent },
  { path: 'cliente', component: AddClientesComponent },





];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

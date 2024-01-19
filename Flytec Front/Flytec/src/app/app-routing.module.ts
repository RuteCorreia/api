import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
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
import { AddCombateIncendioComponent } from './components/combate-incendio/add-combate-incendio/add-combate-incendio.component';
import { AddCombateIncendioDecolagemPousoComponent } from './components/combate-incendio-decolagem-pouso/add-combate-incendio-decolagem-pouso/add-combate-incendio-decolagem-pouso.component';
import { AddCombustivelComponent } from './components/combustivel/add-combustivel/add-combustivel.component';
import { AddControleDeFrotaComponent } from './components/controle-de-frota/add-controle-de-frota/add-controle-de-frota.component';
import { ClientesListComponent } from './components/clientes/clientes-list/clientes-list.component';
import { ClientesDetailsComponent } from './components/clientes/clientes-details/clientes-details.component';
import { AddProdutoComponent } from './components/produto/add-produto/add-produto.component';
import { ProdutoListComponent } from './components/produto/produto-list/produto-list.component';
import { ProdutoDetailsComponent } from './components/produto/produto-details/produto-details.component';
import { AeronaveListComponent } from './components/aeronave/aeronave-list/aeronave-list.component';
import { AeronaveDetailsComponent } from './components/aeronave/aeronave-details/aeronave-details.component';
import { EquipamentoListComponent } from './components/equipamento/equipamento-list/equipamento-list.component';
import { AddEquipamentoComponent } from './components/equipamento/add-equipamento/add-equipamento.component';
import { EquipamentoDetailsComponent } from './components/equipamento/equipamento-details/equipamento-details.component';
import { PistaListComponent } from './components/pista/pista-list/pista-list.component';
import { AddPistaComponent } from './components/pista/add-pista/add-pista.component';
import { PistaDetailsComponent } from './components/pista/pista-details/pista-details.component';
import { AlvoBiologicoListComponent } from './components/alvo-biologico/alvo-biologico-list/alvo-biologico-list.component';
import { AlvoBiologicoDetailsComponent } from './components/alvo-biologico/alvo-biologico-details/alvo-biologico-details.component';
import { CulturaListComponent } from './components/cultura/cultura-list/cultura-list.component';
import { AddCulturaComponent } from './components/cultura/add-cultura/add-cultura.component';
import { CulturaDetailsComponent } from './components/cultura/cultura-details/cultura-details.component';
import { AdjuvanteListComponent } from './components/adjuvante/adjuvante-list/adjuvante-list.component';
import { AdjuvanteDetailsComponent } from './components/adjuvante/adjuvante-details/adjuvante-details.component';
import { VeiculanteListComponent } from './components/veiculante/veiculante-list/veiculante-list.component';
import { AddVeiculanteComponent } from './components/veiculante/add-veiculante/add-veiculante.component';
import { VeiculanteDetailsComponent } from './components/veiculante/veiculante-details/veiculante-details.component';
import { EmpresaListComponent } from './components/empresa/empresa-list/empresa-list.component';
import { AddEmpresaComponent } from './components/empresa/add-empresa/add-empresa.component';
import { EmpresaDetailsComponent } from './components/empresa/empresa-details/empresa-details.component';
import { AplicacaoListComponent } from './components/aplicacao/aplicacao-list/aplicacao-list.component';
import { AplicacaoDetailsComponent } from './components/aplicacao/aplicacao-details/aplicacao-details.component';
import { BulaListComponent } from './components/bula/bula-list/bula-list.component';
import { BulaDetailsComponent } from './components/bula/bula-details/bula-details.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'novoUsuario', component: RegisterComponent },
  { path: 'novoCliente', component: AddClientesComponent },
  { path: 'cliente/:id', component: ClientesDetailsComponent },
  { path: 'profile', component: ProfileComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'usuarios', component: UsersListComponent },
  { path: 'usuario/:id', component: UsersDetailsComponent },
  { path: 'dash', component: DashboardComponent },
  { path: 'aeronave', component: AeronaveListComponent },
  { path: 'novaAeronave', component: AddAeronaveComponent },
  { path: 'aeronave/:id', component: AeronaveDetailsComponent },
  { path: 'alturaVoo', component: AddAlturaVooComponent },
  { path: 'aplicacaoAreaTratada', component: AddAplicacaoAreaTratadaComponent },
  { path: 'aplicacaoCaracteristicas', component: AddAplicacaoCaracteristicasComponent },
  { path: 'aplicacaoContrato', component: AddAplicacaoContratoComponent },
  { path: 'aplicacaoCroqui', component: AddAplicacaoCroquiComponent },
  { path: 'aplicacaoCroquiImportacao', component: AddAplicacaoCroquiImportacaoComponent },
  { path: 'aplicacaoLog', component: AddAplicacaoLogComponent },
  { path: 'aplicacaoRecomendacoesTecnicas', component: AddAplicacaoRecomendacoesTecnicasComponent },
  { path: 'aplicacaoRelatorio', component: AddAplicacaoRelatorioComponent },
  { path: 'aplicacaoRelatorioItem', component: AddAplicacaoRelatorioItemComponent },
  { path: 'cidade', component: AddCidadesComponent },
  { path: 'cliente', component: ClientesListComponent },
  { path: 'combateIncendio', component: AddCombateIncendioComponent },
  { path: 'combateIncendioDecolagemPouso', component: AddCombateIncendioDecolagemPousoComponent },
  { path: 'combustivel', component: AddCombustivelComponent },
  { path: 'controleDeFrota', component: AddControleDeFrotaComponent },
  { path: 'produto', component: ProdutoListComponent },
  { path: 'novoProduto', component: AddProdutoComponent },
  { path: 'produto/:id', component: ProdutoDetailsComponent },
  { path: 'equipamento', component: EquipamentoListComponent },
  { path: 'novoEquipamento', component: AddEquipamentoComponent },
  { path: 'equipamento/:id', component: EquipamentoDetailsComponent },
  { path: 'pista', component: PistaListComponent },
  { path: 'novaPista', component: AddPistaComponent },
  { path: 'pista/:id', component: PistaDetailsComponent },
  { path: 'alvoBiologico', component: AlvoBiologicoListComponent },
  { path: 'novoAlvoBiologico', component: AddAlvoBiologicoComponent },
  { path: 'alvoBiologico/:id', component: AlvoBiologicoDetailsComponent },
  { path: 'cultura', component: CulturaListComponent },
  { path: 'novaCultura', component: AddCulturaComponent },
  { path: 'cultura/:id', component: CulturaDetailsComponent },
  { path: 'adjuvante', component: AdjuvanteListComponent },
  { path: 'novoAdjuvante', component: AdjuvanteComponent },
  { path: 'adjuvante/:id', component: AdjuvanteDetailsComponent },
  { path: 'veiculante', component: VeiculanteListComponent },
  { path: 'novoVeiculante', component: AddVeiculanteComponent },
  { path: 'veiculante/:id', component: VeiculanteDetailsComponent },
  { path: 'empresa', component: EmpresaListComponent },
  { path: 'novaEmpresa', component: AddEmpresaComponent },
  { path: 'empresa/:id', component: EmpresaDetailsComponent },
  { path: 'aplicacao', component: AplicacaoListComponent },
  { path: 'novaAplicacao', component: AddAplicacaoComponent },
  { path: 'aplicacao/:id', component: AplicacaoDetailsComponent },
  { path: 'bula', component: BulaListComponent },
  { path: 'novaBula', component: AddBulaComponent },
  { path: 'bula/:id', component: BulaDetailsComponent },



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

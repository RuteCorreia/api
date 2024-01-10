import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { BoardAdminComponent } from './board-admin/board-admin.component';
import { httpInterceptorProviders } from './helpers/http.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddTutorialComponent } from './components/add-user/add-user.component';
import { UsersDetailsComponent } from './components/users-details/users-details.component';
import { UsersListComponent } from './components/users-list/users-list.component';
import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { DashboardComponent } from './dashboard/components/dashboard/dashboard.component';
import { NgChartsModule } from 'ng2-charts';
import { AdjuvanteComponent } from './components/adjuvante/adjuvante.component';
import { AeronaveComponent } from './components/aeronave/aeronave.component';
import { AlturaVooComponent } from './components/altura-voo/altura-voo.component';
import { AlvoBiologicoComponent } from './components/alvo-biologico/alvo-biologico.component';
import { AplicacaoComponent } from './components/aplicacao/aplicacao.component';
import { AplicacaoAreaTratadaComponent } from './components/aplicacao-area-tratada/aplicacao-area-tratada.component';
import { AplicacaoCaracteristicasComponent } from './components/aplicacao-caracteristicas/aplicacao-caracteristicas.component';
import { AplicacaoContratoComponent } from './components/aplicacao-contrato/aplicacao-contrato.component';
import { AplicacaoCroquiComponent } from './components/aplicacao-croqui/aplicacao-croqui.component';
import { AplicacaoCroquiImportacaoComponent } from './components/aplicacao-croqui-importacao/aplicacao-croqui-importacao.component';
import { AplicacaoLogComponent } from './components/aplicacao-log/aplicacao-log.component';
import { AplicacaoRecomendacoesTecnicasComponent } from './components/aplicacao-recomendacoes-tecnicas/aplicacao-recomendacoes-tecnicas.component';
import { AplicacaoRelatorioComponent } from './components/aplicacao-relatorio/aplicacao-relatorio.component';
import { AplicacaoRelatorioItemComponent } from './components/aplicacao-relatorio-item/aplicacao-relatorio-item.component';
import { BulaComponent } from './components/bula/bula.component';
import { CidadesComponent } from './components/cidades/cidades.component';
import { ClientesComponent } from './components/clientes/clientes.component';
import { CombateIncendioComponent } from './components/combate-incendio/combate-incendio.component';
import { CombateIncendioDecolagemPousoComponent } from './components/combate-incendio-decolagem-pouso/combate-incendio-decolagem-pouso.component';
import { CombustivelComponent } from './components/combustivel/combustivel.component';
import { ControleDeFrotaComponent } from './components/controle-de-frota/controle-de-frota.component';
import { CulturaComponent } from './components/cultura/cultura.component';
import { EmpresaComponent } from './components/empresa/empresa.component';
import { EngenheiroComponent } from './components/engenheiro/engenheiro.component';
import { EquipamentoComponent } from './components/equipamento/equipamento.component';
import { EstadosComponent } from './components/estados/estados.component';
import { ExecutorComponent } from './components/executor/executor.component';
import { FrotaComponent } from './components/frota/frota.component';
import { PilotoComponent } from './components/piloto/piloto.component';
import { PistaComponent } from './components/pista/pista.component';
import { PlanoDeContratoComponent } from './components/plano-de-contrato/plano-de-contrato.component';
import { PrecificacaoComponent } from './components/precificacao/precificacao.component';
import { ProdutoComponent } from './components/produto/produto.component';
import { TipoProdutoComponent } from './components/tipo-produto/tipo-produto.component';
import { VeiculanteComponent } from './components/veiculante/veiculante.component';
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
import { AddCulturaComponent } from './components/cultura/add-cultura/add-cultura.component';
import { AddEmpresaComponent } from './components/empresa/add-empresa/add-empresa.component';
import { AddEngenheiroComponent } from './components/engenheiro/add-engenheiro/add-engenheiro.component';
import { AddEquipamentoComponent } from './components/equipamento/add-equipamento/add-equipamento.component';
import { AddEstadosComponent } from './components/estados/add-estados/add-estados.component';
import { AddExecutorComponent } from './components/executor/add-executor/add-executor.component';
import { AddFrotaComponent } from './components/frota/add-frota/add-frota.component';
import { AddPilotoComponent } from './components/piloto/add-piloto/add-piloto.component';
import { AddPistaComponent } from './components/pista/add-pista/add-pista.component';
import { AddPlanoDeContratoComponent } from './components/plano-de-contrato/add-plano-de-contrato/add-plano-de-contrato.component';
import { AddPrecificacaoComponent } from './components/precificacao/add-precificacao/add-precificacao.component';
import { AddProdutoComponent } from './components/produto/add-produto/add-produto.component';
import { AddTipoProdutoComponent } from './components/tipo-produto/add-tipo-produto/add-tipo-produto.component';
import { AddVeiculanteComponent } from './components/veiculante/add-veiculante/add-veiculante.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    ProfileComponent,
    BoardAdminComponent,
    AddTutorialComponent,
    UsersDetailsComponent,
    UsersListComponent,
    DashboardComponent,
    AdjuvanteComponent,
    AeronaveComponent,
    AlturaVooComponent,
    AlvoBiologicoComponent,
    AplicacaoComponent,
    AplicacaoAreaTratadaComponent,
    AplicacaoCaracteristicasComponent,
    AplicacaoContratoComponent,
    AplicacaoCroquiComponent,
    AplicacaoCroquiImportacaoComponent,
    AplicacaoLogComponent,
    AplicacaoRecomendacoesTecnicasComponent,
    AplicacaoRelatorioComponent,
    AplicacaoRelatorioItemComponent,
    BulaComponent,
    CidadesComponent,
    ClientesComponent,
    CombateIncendioComponent,
    CombateIncendioDecolagemPousoComponent,
    CombustivelComponent,
    ControleDeFrotaComponent,
    CulturaComponent,
    EmpresaComponent,
    EngenheiroComponent,
    EquipamentoComponent,
    EstadosComponent,
    ExecutorComponent,
    FrotaComponent,
    PilotoComponent,
    PistaComponent,
    PlanoDeContratoComponent,
    PrecificacaoComponent,
    ProdutoComponent,
    TipoProdutoComponent,
    VeiculanteComponent,
    AddAeronaveComponent,
    AddAlturaVooComponent,
    AddAlvoBiologicoComponent,
    AddAplicacaoComponent,
    AddAplicacaoAreaTratadaComponent,
    AddAplicacaoCaracteristicasComponent,
    AddAplicacaoContratoComponent,
    AddAplicacaoCroquiComponent,
    AddAplicacaoCroquiImportacaoComponent,
    AddAplicacaoLogComponent,
    AddAplicacaoRecomendacoesTecnicasComponent,
    AddAplicacaoRelatorioComponent,
    AddAplicacaoRelatorioItemComponent,
    AddBulaComponent,
    AddCidadesComponent,
    AddClientesComponent,
    AddCombateIncendioComponent,
    AddCombateIncendioDecolagemPousoComponent,
    AddCombustivelComponent,
    AddControleDeFrotaComponent,
    AddCulturaComponent,
    AddEmpresaComponent,
    AddEngenheiroComponent,
    AddEquipamentoComponent,
    AddEstadosComponent,
    AddExecutorComponent,
    AddFrotaComponent,
    AddPilotoComponent,
    AddPistaComponent,
    AddPlanoDeContratoComponent,
    AddPrecificacaoComponent,
    AddProdutoComponent,
    AddTipoProdutoComponent,
    AddVeiculanteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    NgChartsModule
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { 
  constructor(library: FaIconLibrary) {
    library.addIconPacks(fas, far);

  }

}

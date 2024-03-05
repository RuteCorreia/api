import 'dart:io';

import 'package:flutter/widgets.dart';
import 'package:flytec/features/aplications/presentation/pages/aplicacoes_page.dart';
import 'package:flytec/features/aplications/presentation/pages/contrato_page.dart';
import 'package:flytec/features/aplications/presentation/pages/minhas_aplica%C3%A7%C3%B5es.dart';
import 'package:flytec/features/aplications/presentation/pages/relatorio_aplicacao_page.dart';
import 'package:flytec/features/aplications/presentation/pages/report_aplications_page.dart';
import 'package:flytec/features/aplications/presentation/pages/steps/aplication_first_step.dart';
import 'package:flytec/features/aplications_v2/aplications_page.dart';
import 'package:flytec/features/auth/presentation/pages/login_page.dart';
import 'package:flytec/features/home/presentation/pages/home_page.dart';
import 'package:flytec/features/splash/presentation/pages/splash_page.dart';
import 'package:go_router/go_router.dart';

import '../../features/aplications/presentation/pages/add_contratante_page.dart';
import '../../features/aplications/presentation/pages/caracteristica_produto_page.dart';
import '../../features/aplications/presentation/pages/dados_responsavel_page.dart';
import '../../features/aplications/presentation/pages/home_aplications.dart';
import '../../features/aplications/presentation/pages/recomendacoes_tecnicas_page.dart';
import '../../features/aplications/presentation/pages/steps/aplication_second_step.dart';
import '../../features/aplications/presentation/pages/steps/aplication_third_step.dart';
import '../../features/fire_fighting/presentation/pages/home_firefighting.dart';
import '../../features/fire_fighting/presentation/pages/my_activity_page.dart';
import '../../features/fire_fighting/presentation/pages/steps/add_firefighthing_signature.dart';
import '../../features/fire_fighting/presentation/pages/steps/add_firefighting_fourth_step.dart';
import '../../features/fire_fighting/presentation/pages/steps/add_firefighting_second_step.dart';
import '../../features/fire_fighting/presentation/pages/steps/add_firefighting_step_one.dart';
import '../../features/fire_fighting/presentation/pages/steps/add_firefighting_third_step.dart';

final GoRouter router = GoRouter(
  debugLogDiagnostics: true,
  routes: <RouteBase>[
    GoRoute(
      path: '/',
      builder: (BuildContext context, GoRouterState state) {
        return const SplashScreen();
      },
      routes: <RouteBase>[
        GoRoute(
          path: 'login',
          builder: (BuildContext context, GoRouterState state) {
            return const LoginPage();
          },
        ),
        GoRoute(
          path: 'home',
          builder: (BuildContext context, GoRouterState state) {
            return const HomePaga();
          },
        ),
        GoRoute(
          path: 'combateincendio',
          builder: (BuildContext context, GoRouterState state) {
            return const HomeFireFighting();
          },
        ),
        GoRoute(
          path: 'combateIncendioPasso1',
          builder: (BuildContext context, GoRouterState state) {
            return const AddFireFightingStepOne();
          },
        ),
        GoRoute(
          path: 'combateIncendioPasso2',
          builder: (BuildContext context, GoRouterState state) {
            return const AddFireFightingSecondStep();
          },
        ),
        GoRoute(
          path: 'combateIncendioPasso3',
          builder: (BuildContext context, GoRouterState state) {
            return const AddFireFightingThirdStep();
          },
        ),
        GoRoute(
          path: 'combateIncendioPasso4',
          builder: (BuildContext context, GoRouterState state) {
            return const AddFireFightingFourthtep();
          },
        ),
        GoRoute(
          path: 'addsignature',
          builder: (BuildContext context, GoRouterState state) {
            final extra = state.extra as Map<String, dynamic>;
            return AddFireFightingSignatureStep(
              onUpdateSignature: extra['onUpdateSignature'],
            );
          },
        ),
        GoRoute(
          path: 'myactivity',
          builder: (BuildContext context, GoRouterState state) {
            return const MyActivityPage();
          },
        ),
        GoRoute(
          path: 'aplications',
          builder: (BuildContext context, GoRouterState state) {
            // return const HomeAplicationPage();
            return const AplicationsPage();
          },
        ),
        GoRoute(
          path: 'aplicationstep1',
          builder: (BuildContext context, GoRouterState state) {
            final extra = state.extra as Map<String, dynamic>;
            return AplicationFirstStep(
                updateReportList: extra['updateReportList']);
          },
        ),
        GoRoute(
          path: 'aplicationstep2',
          builder: (BuildContext context, GoRouterState state) {
            return const AplicationSecondStep();
          },
        ),
        GoRoute(
          path: 'reportPage',
          builder: (BuildContext context, GoRouterState state) {
            File file = state.extra as File;
            return ReportAplicationsPage(report: file);
          },
        ),
        GoRoute(
          path: 'aplicationstep3',
          builder: (BuildContext context, GoRouterState state) {
            return const AplicationThirdStep();
          },
        ),
        GoRoute(
          path: 'addcontratante',
          builder: (BuildContext context, GoRouterState state) {
            final extra = state.extra as Map<String, dynamic>;
            return AddContratante(
              onAddContratante: extra['onAddContratante'],
            );
          },
        ),
        // GoRoute(
        //   path: 'identificaoarea',
        //   builder: (BuildContext context, GoRouterState state) {
        //     return const IdentificacaoAreaTratamento(updateIdentifyAreaProcess: ,);
        //   },
        // ),
        // GoRoute(
        //   path: 'croquisarea',
        //   builder: (BuildContext context, GoRouterState state) {
        //     return const CroquisAreaCliente();
        //   },
        // ),
        GoRoute(
          path: 'carateristicaproduto',
          builder: (BuildContext context, GoRouterState state) {
            return const CaracteristicaProdutoPage();
          },
        ),
        GoRoute(
          path: 'carateristicaproduto',
          builder: (BuildContext context, GoRouterState state) {
            return const CaracteristicaProdutoPage();
          },
        ),
        GoRoute(
          path: 'reportPage',
          builder: (BuildContext context, GoRouterState state) {
            File file = state.extra as File;
            return ReportAplicationsPage(report: file);
          },
        ),
        GoRoute(
          path: 'recomendacoestecnicas',
          builder: (BuildContext context, GoRouterState state) {
            return const RecomendacoesTecnicasPage();
          },
        ),
        GoRoute(
          path: 'relatorioaplicacao',
          builder: (BuildContext context, GoRouterState state) {
            return const RelatorioAplicacaoPage();
          },
        ),
        GoRoute(
          path: 'aplicacoes',
          builder: (BuildContext context, GoRouterState state) {
            return const AplicacoesPage();
          },
        ),
        GoRoute(
          path: 'minhasaplicacoes',
          builder: (BuildContext context, GoRouterState state) {
            return const MinhasAplicacoes();
          },
        ),

        GoRoute(
          path: 'contrato',
          builder: (BuildContext context, GoRouterState state) {
            return const ContratoPrestacaoService();
          },
        ),
        GoRoute(
          path: 'responsavel',
          builder: (BuildContext context, GoRouterState state) {
            return const DadosResponsavelPage();
          },
        ),
      ],
    ),
  ],
);

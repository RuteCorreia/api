import 'dart:io';

import 'package:flutter/widgets.dart';
import 'package:flytec/features/aplications/aplications_page.dart';
import 'package:flytec/features/aplications/pages/report_aplications_page.dart';
import 'package:flytec/features/auth/presentation/pages/login_page.dart';
import 'package:flytec/features/home/presentation/pages/home_page.dart';
import 'package:flytec/features/manutencao/manutencao_page_list.dart';
import 'package:flytec/features/splash/presentation/pages/splash_page.dart';
import 'package:go_router/go_router.dart';

import '../../features/fire_fighting/presentation/pages/home_firefighting.dart';
import '../../features/fire_fighting/presentation/pages/my_activity_page.dart';
import '../../features/fire_fighting/presentation/pages/steps/add_firefighthing_signature.dart';

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
          path: 'addsignature',
          builder: (BuildContext context, GoRouterState state) {
            final extra = state.extra as Map<String, dynamic>;
            return AddFireFightingSignatureStep(
              onUpdateSignature: extra['onUpdateSignature'],
            );
          },
        ),
        GoRoute(
          path: 'manutencao',
          builder: (BuildContext context, GoRouterState state) {
            // final extra = state.extra as Map<String, dynamic>;
            return const ManutencaoPageList();
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
            final extra = state.extra as Map<String, dynamic>?;
            return AplicationsPage(
              reportAplicationController: extra?['reportAplicationController'],
            );
          },
        ),
        GoRoute(
          path: 'reportPage',
          builder: (BuildContext context, GoRouterState state) {
            File file = state.extra as File;
            return ReportAplicationsPage(report: file);
          },
        ),
      ],
    ),
  ],
);

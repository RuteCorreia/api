import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flytec/features/aplications/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_controller.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_service.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_service_background_locator2.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_store.dart';
import 'package:flytec/features/aplications/pages/rastreamento/tracking_usecase.dart';
import 'package:get_it/get_it.dart';

import 'tracking_map_store.dart';
import 'tracking_time_store.dart';
import 'tracking_view.dart';

class TrackingPage extends StatefulWidget {
  final ReportAplicationController reportApplicationController;
  const TrackingPage({super.key, required this.reportApplicationController});

  @override
  State<TrackingPage> createState() => _TrackingPageState();
}

class _TrackingPageState extends State<TrackingPage> {
  @override
  Widget build(BuildContext context) {
    GetIt getIt = GetIt.instance;
    if (!getIt.isRegistered<TrackingUseCase>()) {
      GetIt.I.registerSingleton<TrackingService>(
          TrackingServiceBackgroundLocator2());
      GetIt.I.registerSingleton<TrackingUseCase>(
          TrackingController(service: getIt()));
      getIt.registerSingleton<TrackingStore>(TrackingStore());
      getIt.registerSingleton<TrackingMapStore>(TrackingMapStore());
      getIt.registerSingleton<TrackingTimeStore>(TrackingTimeStore());
    }

    return MultiBlocProvider(
      providers: [
        BlocProvider<TrackingStore>(
          create: (context) => getIt(),
        ),
        BlocProvider<TrackingMapStore>(
          create: (context) => getIt(),
        ),
        BlocProvider<TrackingTimeStore>(
          create: (context) => getIt(),
        )
      ],
      child: const TrackingView(),
    );
  }
}

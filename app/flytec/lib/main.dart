import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:sizer/sizer.dart';

import './core/injections/get_it.dart' as injections;
import 'core/routes/routes_config.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();
  SystemChrome.setPreferredOrientations([DeviceOrientation.portraitUp]);
  injections.setup();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return Sizer(builder: (context, orientation, deviceType) {
      return MaterialApp.router(
        debugShowCheckedModeBanner: false,
        title: 'FlyTec',
        localizationsDelegates: const [
          GlobalMaterialLocalizations.delegate,
          GlobalWidgetsLocalizations.delegate,
          GlobalCupertinoLocalizations.delegate
        ],
        supportedLocales: const [Locale("pt")],
        theme: ThemeData(
          colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
          appBarTheme: AppBarTheme(
            backgroundColor: Colors.primaries[9],
            titleTextStyle: const TextStyle(
              color: Colors.white,
              fontWeight: FontWeight.w500,
              fontSize: 20,
            ),
            iconTheme: const IconThemeData(
              color: Colors.white,
            ),
          ),
          datePickerTheme: DatePickerThemeData(
            todayBackgroundColor: MaterialStateProperty.all(Colors.green),
            inputDecorationTheme: const InputDecorationTheme(
              labelStyle: TextStyle(color: Colors.red),
            ),
          ),
          floatingActionButtonTheme: FloatingActionButtonThemeData(
            backgroundColor: Colors.primaries[9],
            elevation: 2,
          ),
          checkboxTheme: CheckboxThemeData(
            checkColor: MaterialStateProperty.all(Colors.white),
            fillColor: MaterialStateProperty.all(Colors.green),
          ),
          dialogTheme: const DialogTheme(
            backgroundColor: Color(0XFFECEAEA),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.all(
                Radius.circular(10),
              ),
            ),
          ),
          useMaterial3: true,
        ),
        routerConfig: router,
      );
    });
  }
}

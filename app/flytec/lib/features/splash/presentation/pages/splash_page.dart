import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/save_local_controller.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/auth/data/models/user_payload_model.dart';
import 'package:flytec/features/auth/service/auth_service.dart';
import 'package:go_router/go_router.dart';
import 'package:jwt_decoder/jwt_decoder.dart';

class SplashScreen extends StatefulWidget {
  const SplashScreen({super.key});

  @override
  State<SplashScreen> createState() => _SplashScreenState();
}

class _SplashScreenState extends State<SplashScreen> {
  @override
  void initState() {
    super.initState();

    Future.delayed(const Duration(seconds: 0), () {
      getIt<AuthService>().getToken().then((token) async {
        if (token.isNotEmpty) {
          bool hasExpired = JwtDecoder.isExpired(token);
          if (!hasExpired) {
            Map<String, dynamic> decodedToken = JwtDecoder.decode(token);
            Util.Token = token;

            getIt<SaveLocalDataController>()
                .getLocalPreloadData()
                .then((value) {
              getIt<GlobalConfigVars>().setPreloadDataFromJson(
                preloadJson: jsonDecode(value.toString()),
              );
            });
            getIt<GlobalConfigVars>().userPayload =
                UserPayloadModel.fromJson(decodedToken);

            context.pushReplacement("/home");
          } else {
            context.pushReplacement("/login");
          }
        } else {
          context.pushReplacement("/login");
        }
      });
    });
  }

  @override
  void didChangeDependencies() async {
    super.didChangeDependencies();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 226, 220, 220),
      //backgroundColor: Colors.red,

      body: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          Image.asset(
            "assets/images/logo.png",
            width: 160,
            height: 160,
          ),
          const SizedBox(height: 20),
          const Center(
            child: SizedBox(
              width: 40,
              height: 40,
              child: CircularProgressIndicator(
                backgroundColor: Colors.red,
              ),
            ),
          )
        ],
      ),
    );
  }
}

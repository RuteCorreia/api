import 'package:flutter/material.dart';

class CustomAuthLogo extends StatelessWidget {
  const CustomAuthLogo({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      width: 240,
      height: 200,
      decoration: const BoxDecoration(
        image: DecorationImage(
          image: AssetImage("assets/images/logo.png"),
          fit: BoxFit.contain,
        ),
      ),
    );
  }
}

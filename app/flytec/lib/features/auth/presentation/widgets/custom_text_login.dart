import 'package:flutter/material.dart';

class CustomTextLogin extends StatelessWidget {
  const CustomTextLogin({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return const SizedBox(
      width: 250,
      child: Text.rich(
        TextSpan(
          children: [
            TextSpan(
              text: 'Faça o ',
              style: TextStyle(
                color: Color.fromARGB(255, 121, 118, 118),
                fontSize: 17.62,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w500,
              ),
            ),
            TextSpan(
              text: 'login',
              style: TextStyle(
                color: Color(0xFF00B45D),
                fontSize: 17.62,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w700,
              ),
            ),
            TextSpan(
              text: ' para acessar',
              style: TextStyle(
                color: Color.fromARGB(255, 121, 118, 118),
                fontSize: 17.62,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w500,
              ),
            ),
          ],
        ),
        textAlign: TextAlign.center,
      ),
    );
  }
}

import 'package:flutter/material.dart';

class WelcomeText extends StatelessWidget {
  const WelcomeText({super.key, required this.userName});

  final String userName;
  @override
  Widget build(BuildContext context) {

    return SizedBox(
      width: 350,
      height: 50,
      child: Text.rich(
        TextSpan(
          children: [
            const TextSpan(
              text: 'Bem vindo, ',
              style: TextStyle(
                color: Color.fromARGB(255, 121, 118, 118),
                fontSize: 20,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w600,
              ),
            ),
           
            TextSpan(
              text: userName,
              style: const TextStyle(
                color: Color(0xFF00B45D),
                fontSize: 20,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w800,
              ),
            ),
          ],
        ),
      ),
    );
  }
}

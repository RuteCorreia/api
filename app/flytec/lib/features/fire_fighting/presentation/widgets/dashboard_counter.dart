import 'package:flutter/material.dart';

import '../pages/home_firefighting.dart';

class CustomDashBoardCounter extends StatelessWidget {
  const CustomDashBoardCounter(
      {super.key,
      required this.value,
      required this.text,
      required this.state});
  final String value;
  final String text;
  final DashBoardState state;
  Color getColorStateColor(DashBoardState estado) {
    if (estado == DashBoardState.Enviado) {
      return Colors.blue;
    }
    if (estado == DashBoardState.Pronto) {
      return Colors.green;
    }
    if (estado == DashBoardState.Incompleto) {
      return const Color(0xff0ffff9900);
    }
    if (estado == DashBoardState.NaoEnviado) {
      return Colors.red;
    } else {
      return Colors.blue;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      width: 74.50,
      height: 80,
      padding: const EdgeInsets.all(4),
      clipBehavior: Clip.antiAlias,
      decoration: ShapeDecoration(
        shape: RoundedRectangleBorder(
          side: BorderSide(width: 2, color: getColorStateColor(state)),
          borderRadius: BorderRadius.circular(8),
        ),
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          SizedBox(
            width: double.infinity,
            child: Text(
              value,
              style: const TextStyle(
                color: Color.fromARGB(255, 121, 118, 118),
                fontSize: 24,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w700,
                height: 0.06,
              ),
            ),
          ),
          const SizedBox(height: 20),
          SizedBox(
            width: double.infinity,
            child: Row(
              mainAxisSize: MainAxisSize.min,
              mainAxisAlignment: MainAxisAlignment.start,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Expanded(
                  child: SizedBox(
                    child: Text(
                      text,
                      style: const TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 10,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w700,
                        height: 0.15,
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

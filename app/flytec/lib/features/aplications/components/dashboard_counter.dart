import 'package:flutter/material.dart';
import 'package:flytec/features/aplications/enums/report_dashboard_state.dart';

class ReportDashBoardCounter extends StatelessWidget {
  const ReportDashBoardCounter(
      {super.key,
      required this.value,
      required this.text,
      required this.state});
  final String value;
  final String text;
  final ReportDashBoardState state;
  Color getColorStateColor(ReportDashBoardState estado) {
    if (estado == ReportDashBoardState.Enviado) {
      return Colors.blue;
    }
    if (estado == ReportDashBoardState.Pronto) {
      return Colors.green;
    }
    if (estado == ReportDashBoardState.Incompleto) {
      return const Color(0xff0ffff9900);
    }
    if (estado == ReportDashBoardState.NaoEnviado) {
      return Colors.red;
    } else {
      return Colors.blue;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      width: MediaQuery.of(context).size.width * 0.22,
      height: MediaQuery.of(context).size.height * 0.15,
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
          Text(
            value,
            maxLines: 1,
            overflow: TextOverflow.ellipsis,
            style: const TextStyle(
              color: Color.fromARGB(255, 121, 118, 118),
              fontSize: 24,
              fontFamily: 'Inter',
              fontWeight: FontWeight.w700,
            ),
          ),
          Flexible(
            flex: 1,
            child: Text(
              text,
              maxLines: 2,
              style: const TextStyle(
                color: Color.fromARGB(255, 121, 118, 118),
                fontSize: 10,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w700,
              ),
            ),
          ),
        ],
      ),
    );
  }
}

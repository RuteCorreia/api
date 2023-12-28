import 'package:flutter/material.dart';

class SpeedWindSelect extends StatelessWidget {
  final Function(String) onChangedSpeedWind;
  SpeedWindSelect({super.key, required this.onChangedSpeedWind});
  final List<int> _speeds = List.generate(1001, (index) => index);
  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 300,
      child: ListView.builder(
        itemCount: _speeds.length,
        shrinkWrap: true,
        padding: EdgeInsets.zero,
        itemBuilder: (context, index) => MaterialButton(
            shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(8.0)),
            padding: EdgeInsets.zero,
            color: Colors.white,
            elevation: 0,
            onPressed: () {
              onChangedSpeedWind('${_speeds[index]} Km/h');
              Navigator.of(context).pop();
            },
            child: Align(
                alignment: Alignment.centerLeft,
                child: Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Text("${_speeds[index]} Km/h",
                      style: const TextStyle(
                          color: Colors.black,
                          fontSize: 16,
                          fontWeight: FontWeight.w600)),
                ))),
      ),
    );
  }
}

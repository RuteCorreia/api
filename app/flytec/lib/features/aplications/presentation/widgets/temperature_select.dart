import 'package:flutter/material.dart';

class TemperatureSelect extends StatelessWidget {
  final Function(String) onChangedTemperature;
  TemperatureSelect({super.key, required this.onChangedTemperature});

  final List<double> _temperatures = List.generate(201, (index) => index / 2);

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 300,
      child: ListView.builder(
        itemCount: _temperatures.length,
        shrinkWrap: true,
        padding: EdgeInsets.zero,
        itemBuilder: (context, index) => MaterialButton(
            shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(8.0)),
            padding: EdgeInsets.zero,
            color: Colors.white,
            elevation: 0,
            onPressed: () {
              onChangedTemperature('${_temperatures[index]}°C');
              Navigator.of(context).pop();
            },
            child: Align(
                alignment: Alignment.centerLeft,
                child: Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Text("${_temperatures[index]}°C",
                      style: const TextStyle(
                          color: Colors.black,
                          fontSize: 16,
                          fontWeight: FontWeight.w600)),
                ))),
      ),
    );
  }
}

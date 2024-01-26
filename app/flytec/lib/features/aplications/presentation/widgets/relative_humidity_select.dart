import 'package:flutter/material.dart';

class RelativeHumiditySelect extends StatelessWidget {
  final Function(String) onChangedHumidity;
  RelativeHumiditySelect({super.key, required this.onChangedHumidity});

  final List<int> _relativeHumidity = List.generate(46, (index) => index + 55);

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 300,
      child: ListView.builder(
        itemCount: _relativeHumidity.length,
        shrinkWrap: true,
        padding: EdgeInsets.zero,
        itemBuilder: (context, index) => MaterialButton(
            shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(8.0)),
            padding: EdgeInsets.zero,
            color: Colors.white,
            elevation: 0,
            onPressed: () {
              onChangedHumidity('+ ${_relativeHumidity[index]}%');
              Navigator.of(context).pop();
            },
            child: Align(
                alignment: Alignment.centerLeft,
                child: Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Text("+ ${_relativeHumidity[index]}%",
                      style: const TextStyle(
                          color: Colors.black,
                          fontSize: 16,
                          fontWeight: FontWeight.w600)),
                ))),
      ),
    );
  }
}

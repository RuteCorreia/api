import 'package:flutter/material.dart';

class DegreeSelect extends StatelessWidget {
  final Function(String) onChangeDegree;
  DegreeSelect({super.key, required this.onChangeDegree});
  final List<int> _degree = List.generate(201, (index) => index * 5);

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 300,
      child: ListView.builder(
        itemCount: _degree.length,
        shrinkWrap: true,
        padding: EdgeInsets.zero,
        itemBuilder: (context, index) => MaterialButton(
            shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(8.0)),
            padding: EdgeInsets.zero,
            color: Colors.white,
            elevation: 0,
            onPressed: () {
              onChangeDegree('${_degree[index]}°');
              Navigator.of(context).pop();
            },
            child: Align(
                alignment: Alignment.centerLeft,
                child: Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Text("${_degree[index]}°",
                      style: const TextStyle(
                          color: Colors.black,
                          fontSize: 16,
                          fontWeight: FontWeight.w600)),
                ))),
      ),
    );
  }
}

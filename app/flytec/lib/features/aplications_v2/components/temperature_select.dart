import 'package:flutter/material.dart';

class TemperatureSelect extends StatefulWidget {
  final Function(String) onChangedTemperature;
  final bool scrollTheList;
  final int scrollToIndex;
  const TemperatureSelect(
      {super.key,
      required this.onChangedTemperature,
      this.scrollTheList = false,
      this.scrollToIndex = 0});

  @override
  State<TemperatureSelect> createState() => _TemperatureSelectState();
}

class _TemperatureSelectState extends State<TemperatureSelect> {
  final List<double> _temperatures = List.generate(63, (index) => index / 2);

  final ScrollController _scrollController = ScrollController();
  void scrollToItem(int index) {
    if (_scrollController.hasClients) {
      _scrollController.animateTo(
        index * 101.0, // Multiplica o índice pela altura do item
        duration: const Duration(seconds: 1),
        curve: Curves.ease,
      );
    }
  }

  @override
  void initState() {
    super.initState();
    if (widget.scrollTheList) {
      Future.delayed(const Duration(seconds: 0), () {
        scrollToItem(widget.scrollToIndex);
        setState(() {});
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 300,
      child: ListView.builder(
        controller: _scrollController,
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
              widget.onChangedTemperature('${_temperatures[index]}°C');
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

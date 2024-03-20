import 'package:flutter/material.dart';

class SpeedWindSelect extends StatefulWidget {
  final Function(String) onChangedSpeedWind;
  final bool scrollTheList;
  final int scrollToIndex;
  const SpeedWindSelect(
      {super.key,
      required this.onChangedSpeedWind,
      this.scrollTheList = false,
      this.scrollToIndex = 0});

  @override
  State<SpeedWindSelect> createState() => _SpeedWindSelectState();
}

class _SpeedWindSelectState extends State<SpeedWindSelect> {
  final List<int> _speeds = List.generate(1001, (index) => index);

  final ScrollController _scrollController = ScrollController();
  void scrollToItem(int index) {
    if (_scrollController.hasClients) {
      _scrollController.animateTo(
        index * 38, // Multiplica o índice pela altura do item
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
        itemCount: _speeds.length,
        shrinkWrap: true,
        padding: EdgeInsets.zero,
        itemBuilder: (context, index) => Padding(
          padding: const EdgeInsets.symmetric(vertical:4.0),
          child: SizedBox(
            height: 38,
            child: MaterialButton(
                shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(8.0)),
                padding: EdgeInsets.zero,
                color: Colors.white,
                elevation: 0,
                onPressed: () {
                  widget.onChangedSpeedWind('${_speeds[index]} Km/h');
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
        ),
      ),
    );
  }
}

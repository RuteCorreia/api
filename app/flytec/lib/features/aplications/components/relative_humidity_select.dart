import 'package:flutter/material.dart';

class RelativeHumiditySelect extends StatefulWidget {
  final Function(String) onChangedHumidity;
  final bool scrollTheList;
  final int scrollToIndex;
  const RelativeHumiditySelect(
      {super.key,
      required this.onChangedHumidity,
      this.scrollTheList = false,
      this.scrollToIndex = 0});

  @override
  State<RelativeHumiditySelect> createState() => _RelativeHumiditySelectState();
}

class _RelativeHumiditySelectState extends State<RelativeHumiditySelect> {
  final List<int> _relativeHumidity = List.generate(101, (index) => index);

  final ScrollController _scrollController = ScrollController();
  void scrollToItem(int index) {
    if (_scrollController.hasClients) {
      _scrollController.animateTo(
        index * 49, // Multiplica o índice pela altura do item
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
        itemCount: _relativeHumidity.length,
        shrinkWrap: true,
        padding: EdgeInsets.zero,
        itemBuilder: (context, index) => Padding(
            padding: const EdgeInsets.symmetric(vertical: 4.0),
            child: MaterialButton(
                shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(8.0)),
                padding: EdgeInsets.zero,
                color: Colors.white,
                elevation: 0,
                onPressed: () {
                  widget.onChangedHumidity('+ ${_relativeHumidity[index]}%');
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
                    )))),
      ),
    );
  }
}

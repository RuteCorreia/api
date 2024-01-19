import 'package:flutter/material.dart';

class ObservationsSelect extends StatelessWidget {
  final Function(String, int) onChangedObservations;
  final Function(int) findObservation;
  final List<int> observationsIndex;
  ObservationsSelect(
      {super.key,
      required this.onChangedObservations,
      required this.findObservation,
      required this.observationsIndex});

  String _observationsIndex(int index) {
    return 'LOG ${observationsIndex[index].toString().padLeft(3, '0')}';
  }

  final TextEditingController _observationText = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: MediaQuery.of(context).size.height * 0.35,
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          SizedBox(
            height: MediaQuery.of(context).size.height * 0.25,
            child: ListView.builder(
              itemCount: observationsIndex.length,
              shrinkWrap: true,
              padding: EdgeInsets.zero,
              itemBuilder: (context, index) => MaterialButton(
                  shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(8.0)),
                  padding: EdgeInsets.zero,
                  color: Colors.white,
                  elevation: 0,
                  onPressed: () {
                    onChangedObservations(_observationsIndex(index), index);
                    Navigator.of(context).pop();
                  },
                  child: Align(
                      alignment: Alignment.centerLeft,
                      child: Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: Text(_observationsIndex(index),
                            style: const TextStyle(
                                color: Colors.black,
                                fontSize: 16,
                                fontWeight: FontWeight.w600)),
                      ))),
            ),
          ),
          const SizedBox(height: 14),
          SizedBox(
            height: 40,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Container(
                  width: MediaQuery.of(context).size.width / 2.1,
                  alignment: Alignment.topCenter,
                  padding: const EdgeInsets.symmetric(horizontal: 8.0),
                  decoration: ShapeDecoration(
                    shape: RoundedRectangleBorder(
                      side:
                          const BorderSide(width: 1, color: Color(0xFF636363)),
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                  child: TextField(
                    controller: _observationText,
                    keyboardType: TextInputType.number,
                    onSubmitted: (value) {
                      findObservation(int.tryParse(_observationText.text)!);
                      Navigator.of(context).pop();
                    },
                    decoration: const InputDecoration(
                        hintText: "Digite um LOG",
                        border: InputBorder.none,
                        hintStyle: TextStyle(
                          color: Color.fromARGB(255, 121, 118, 118),
                          fontSize: 14,
                          fontFamily: 'Inter',
                          fontWeight: FontWeight.w500,
                        )),
                  ),
                ),
                InkWell(
                  onTap: () {
                    findObservation(int.tryParse(_observationText.text)!);
                    Navigator.of(context).pop();
                  },
                  child: Container(
                    width: MediaQuery.of(context).size.width * 0.14,
                    height: MediaQuery.of(context).size.width * 0.12,
                    padding: const EdgeInsets.all(1),
                    clipBehavior: Clip.antiAlias,
                    decoration: ShapeDecoration(
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(8.0),
                      ),
                      color: Colors.green,
                    ),
                    child: const Column(
                      mainAxisSize: MainAxisSize.min,
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Icon(
                          Icons.check,
                          color: Colors.white,
                        )
                      ],
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

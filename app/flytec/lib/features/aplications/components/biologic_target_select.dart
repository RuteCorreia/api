import 'package:flutter/material.dart';

class BiologicTargetSelect extends StatelessWidget {
  final Function(String) onChanged;
  BiologicTargetSelect({super.key, required this.onChanged});

  final List<String> _biologicTarget = [
    'Cercosporiose',
    'Mancha-de-Phaeosphaeria',
    'Ferrugem-Polisora'
  ];

  @override
  Widget build(BuildContext context) {
    return SizedBox(
        height: 180,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            SizedBox(
              height: 160,
              child: ListView.builder(
                itemCount: _biologicTarget.length,
                shrinkWrap: true,
                padding: EdgeInsets.zero,
                itemBuilder: (context, index) => MaterialButton(
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(8.0)),
                    padding: EdgeInsets.zero,
                    color: Colors.white,
                    elevation: 0,
                    onPressed: () {
                      onChanged(_biologicTarget[index]);
                      Navigator.of(context).pop();
                    },
                    child: Align(
                        alignment: Alignment.centerLeft,
                        child: Padding(
                          padding: const EdgeInsets.all(8.0),
                          child: Text(_biologicTarget[index],
                              style: const TextStyle(
                                  color: Colors.black,
                                  fontSize: 16,
                                  fontWeight: FontWeight.w600)),
                        ))),
              ),
            ),
          ],
        ));
  }
}

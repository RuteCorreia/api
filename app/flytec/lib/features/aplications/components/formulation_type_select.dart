import 'package:flutter/material.dart';

class FormulationTypeSelect extends StatelessWidget {
  final Function(String) onChangedFormulationType;
  FormulationTypeSelect({super.key, required this.onChangedFormulationType});

  final TextEditingController _textFlightHeight = TextEditingController();

  final List<String> _formulationType = [
    'Concentrado emulsionável (EC)',
    'Suspensão concentrada (SC)',
    'Grânulos dispersíveis em água'
  ];

  @override
  Widget build(BuildContext context) {
    return SizedBox(
        height: MediaQuery.of(context).size.height * 0.385,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            SizedBox(
              height: MediaQuery.of(context).size.height * 0.27,
              child: ListView.builder(
                itemCount: _formulationType.length,
                shrinkWrap: true,
                padding: EdgeInsets.zero,
                itemBuilder: (context, index) => Padding(
                  padding: const EdgeInsets.symmetric(vertical:4.0),
                  child: MaterialButton(
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(8.0)),
                      padding: EdgeInsets.zero,
                      color: Colors.white,
                      elevation: 0,
                      onPressed: () {
                        onChangedFormulationType(_formulationType[index]);
                        Navigator.of(context).pop();
                      },
                      child: Align(
                          alignment: Alignment.centerLeft,
                          child: Padding(
                            padding: const EdgeInsets.all(8.0),
                            child: Text(_formulationType[index],
                                style: const TextStyle(
                                    color: Colors.black,
                                    fontSize: 16,
                                    fontWeight: FontWeight.w600)),
                          ))),
                ),
              ),
            ),
            SizedBox(
              height: 40,
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Container(
                    width: MediaQuery.of(context).size.width / 2.25,
                    alignment: Alignment.topCenter,
                    padding: const EdgeInsets.symmetric(horizontal: 8.0),
                    decoration: ShapeDecoration(
                      shape: RoundedRectangleBorder(
                        side: const BorderSide(
                            width: 1, color: Color(0xFF636363)),
                        borderRadius: BorderRadius.circular(10),
                      ),
                    ),
                    child: TextField(
                      controller: _textFlightHeight,
                      keyboardType: TextInputType.text,
                      onSubmitted: (value) {
                        onChangedFormulationType(_textFlightHeight.text);
                        Navigator.of(context).pop();
                      },
                      decoration: const InputDecoration(
                          hintText: "Digite um tipo",
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
                      onChangedFormulationType(_textFlightHeight.text);
                      Navigator.of(context).pop();
                    },
                    child: Container(
                      width: MediaQuery.of(context).size.width * 0.135,
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
        ));
  }
}

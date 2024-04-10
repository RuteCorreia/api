import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';

class AirCraftSelect extends StatelessWidget {
  final Function(String) onChanged;
  AirCraftSelect({super.key, required this.onChanged});

  final TextEditingController _textFlightHeight = TextEditingController();


  @override
  Widget build(BuildContext context) {
    return SizedBox(
        height: MediaQuery.of(context).size.height * 0.285,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            SizedBox(
              height: MediaQuery.of(context).size.height * 0.2,
              child: ListView.builder(
                itemCount: getIt<GlobalConfigVars>().aeronaves.length,
                shrinkWrap: true,
                padding: EdgeInsets.zero,
                itemBuilder: (context, index) => MaterialButton(
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(8.0)),
                    padding: EdgeInsets.zero,
                    color: Colors.white,
                    elevation: 0,
                    onPressed: () {
                      onChanged(
                          getIt<GlobalConfigVars>().aeronaves[index].prefixo!);
                      Navigator.of(context).pop();
                    },
                    child: Align(
                        alignment: Alignment.centerLeft,
                        child: Padding(
                          padding: const EdgeInsets.all(8.0),
                          child: Text(
                              getIt<GlobalConfigVars>()
                                      .aeronaves[index]
                                      .prefixo ??
                                  '',
                              style: const TextStyle(
                                  color: Colors.black,
                                  fontSize: 16,
                                  fontWeight: FontWeight.w600)),
                        ))),
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
                        onChanged(_textFlightHeight.text);
                        Navigator.of(context).pop();
                      },
                      decoration: const InputDecoration(
                          hintText: "Digite uma aeronave",
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
                      onChanged(_textFlightHeight.text);
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

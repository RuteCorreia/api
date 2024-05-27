import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/features/alvo_biologico/data/models/alvo_biologico_model.dart';

class BiologicTargetSelect extends StatelessWidget {
  final Function(String) onChanged;
  final List<AlvoBiologicoModel>? alvosBiologicosSecondary;
  BiologicTargetSelect({
    super.key,
    required this.onChanged,
    required this.alvosBiologicosSecondary,
  });

  final TextEditingController _textEditingController = TextEditingController();

  List<AlvoBiologicoModel> get _alvosBiologicos =>
      alvosBiologicosSecondary != null && alvosBiologicosSecondary!.isNotEmpty
          ? alvosBiologicosSecondary!
          : getIt<GlobalConfigVars>().alvosBiologicos;

  @override
  Widget build(BuildContext context) {
    return SizedBox(
        height: 200,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            SizedBox(
              height: 160,
              child: ListView.builder(
                itemCount: _alvosBiologicos.length,
                shrinkWrap: true,
                padding: EdgeInsets.zero,
                itemBuilder: (context, index) => MaterialButton(
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(8.0)),
                    padding: EdgeInsets.zero,
                    color: Colors.white,
                    elevation: 0,
                    onPressed: () {
                      onChanged(_alvosBiologicos[index].nome!);
                      Navigator.of(context).pop();
                    },
                    child: Align(
                        alignment: Alignment.centerLeft,
                        child: Padding(
                          padding: const EdgeInsets.all(8.0),
                          child: Text(_alvosBiologicos[index].nome!,
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
                      controller: _textEditingController,
                      keyboardType: TextInputType.text,
                      onSubmitted: (value) {
                        onChanged(_textEditingController.text);
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
                      onChanged(_textEditingController.text);
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

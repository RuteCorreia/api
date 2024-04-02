import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';

class BiologicTargetSelect extends StatelessWidget {
  final Function(String) onChanged;
  const BiologicTargetSelect({super.key, required this.onChanged});

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
                itemCount: getIt<GlobalConfigVars>().alvosBiologicos .length,
                shrinkWrap: true,
                padding: EdgeInsets.zero,
                itemBuilder: (context, index) => MaterialButton(
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(8.0)),
                    padding: EdgeInsets.zero,
                    color: Colors.white,
                    elevation: 0,
                    onPressed: () {
                      onChanged(getIt<GlobalConfigVars>().alvosBiologicos[index].nome!);
                      Navigator.of(context).pop();
                    },
                    child: Align(
                        alignment: Alignment.centerLeft,
                        child: Padding(
                          padding: const EdgeInsets.all(8.0),
                          child: Text(getIt<GlobalConfigVars>().alvosBiologicos[index].nome!,
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

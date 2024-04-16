import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';

class ExecutorSelect extends StatelessWidget {
  final Function(String?) onChanged;
  const ExecutorSelect({super.key, required this.onChanged});

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
                itemCount: getIt<GlobalConfigVars>().executores.length,
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
                          onChanged(
                            getIt<GlobalConfigVars>().executores[index].nome!,
                          );
                          Navigator.of(context).pop();
                        },
                        child: Align(
                            alignment: Alignment.centerLeft,
                            child: Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: Text(
                                  getIt<GlobalConfigVars>()
                                      .executores[index]
                                      .nome!,
                                  style: const TextStyle(
                                      color: Colors.black,
                                      fontSize: 16,
                                      fontWeight: FontWeight.w600)),
                            )))),
              ),
            ),
          ],
        ));
  }
}

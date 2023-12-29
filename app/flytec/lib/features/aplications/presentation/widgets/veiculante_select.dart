import 'package:flutter/material.dart';

class VeiculanteSelect extends StatelessWidget {
  final Function(String) onChangeVeiculanteType;
  VeiculanteSelect({super.key, required this.onChangeVeiculanteType});
  final List<String> _veiculanteType = ['Água', 'Óleo', 'Nenhum'];

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 150,
      child: Align(
        alignment: Alignment.center,
        child: ListView.builder(
          itemCount: _veiculanteType.length,
          shrinkWrap: true,
          padding: EdgeInsets.zero,
          itemBuilder: (context, index) => MaterialButton(
              shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(8.0)),
              padding: EdgeInsets.zero,
              color: Colors.white,
              elevation: 0,
              onPressed: () {
                onChangeVeiculanteType(_veiculanteType[index]);
                Navigator.of(context).pop();
              },
              child: Align(
                  alignment: Alignment.centerLeft,
                  child: Padding(
                    padding: const EdgeInsets.all(8.0),
                    child: Text(_veiculanteType[index],
                        style: const TextStyle(
                            color: Colors.black,
                            fontSize: 16,
                            fontWeight: FontWeight.w600)),
                  ))),
        ),
      ),
    );
  }
}

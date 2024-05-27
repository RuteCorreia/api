import 'package:flutter/material.dart';

class ClassificacaoModel {
  final String nome;
  final String categoria;
  final Color cor;
  ClassificacaoModel(
      {required this.nome, required this.cor, required this.categoria});
}

class ToxicologicalClassificationSelect extends StatelessWidget {
  final Function(int?) onSelect;
  final List<ClassificacaoModel> _classfication;
  const ToxicologicalClassificationSelect(
      {super.key,
      required this.onSelect,
      required List<ClassificacaoModel> classfication})
      : _classfication = classfication;

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          SizedBox(
            child: ListView.builder(
              itemCount: _classfication.length,
              shrinkWrap: true,
              padding: EdgeInsets.zero,
              itemBuilder: (context, index) => Container(
                height: 60,
                margin: const EdgeInsets.only(top: 10),
                decoration: BoxDecoration(
                    color: Colors.white,
                    border: Border.all(
                      color: _classfication[index].cor,
                      width: 3,
                    )),
                child: TextButton(
                    onPressed: () {
                      onSelect(index);
                      Navigator.of(context).pop();
                    },
                    child: Padding(
                      padding: const EdgeInsets.symmetric(
                        horizontal: 2,
                        vertical: 2,
                      ),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            "CATEGORIA ${index + 1}",
                            style: const TextStyle(
                              fontSize: 10,
                            ),
                          ),
                          const SizedBox(height: 2),
                          Center(
                            child: Text(
                              _classfication[index].nome,
                              style: const TextStyle(
                                color: Colors.black,
                                fontSize: 11,
                                fontWeight: FontWeight.w600,
                              ),
                            ),
                          ),
                        ],
                      ),
                    )),
              ),
            ),
          ),
        ],
      ),
    );
  }
}

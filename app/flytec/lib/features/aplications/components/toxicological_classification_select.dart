import 'package:flutter/material.dart';

class ClassificacaoModel {
  final String nome;
  final String categoria;
  final Color cor;
  ClassificacaoModel(
      {required this.nome, required this.cor, required this.categoria});
}

class ToxicologicalClassificationSelect extends StatelessWidget {
  final Function(String) onSelect;
  ToxicologicalClassificationSelect({super.key, required this.onSelect});

  final List<ClassificacaoModel> _classfication = [
    ClassificacaoModel(
        nome: 'Extremamente tóxico', cor: Colors.red, categoria: "Categoria 1"),
    ClassificacaoModel(
        nome: 'Altamente tóxico', cor: Colors.red, categoria: "Categoria 2"),
    ClassificacaoModel(
        nome: 'Moderadamente tóxico',
        cor: Colors.yellow,
        categoria: "Categoria 3"),
    ClassificacaoModel(
        nome: 'Pouco tóxico', cor: Colors.blue, categoria: "Categoria 4"),
    ClassificacaoModel(
        nome: 'Improvável de causar dano agudo ',
        cor: Colors.blue,
        categoria: "Categoria 5"),
    ClassificacaoModel(
        nome: 'Não classificado', cor: Colors.green, categoria: "Categoria 6"),
  ];

  @override
  Widget build(BuildContext context) {
    return Container(
        child: SingleChildScrollView(
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
                      onSelect(_classfication[index].categoria);
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
    ));
  }
}

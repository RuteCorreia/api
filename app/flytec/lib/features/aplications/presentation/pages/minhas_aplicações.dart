import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/features/aplications/presentation/pages/steps/aplication_second_step.dart';
import 'package:go_router/go_router.dart';

class MinhasAplicacoes extends StatefulWidget {
  const MinhasAplicacoes({super.key});

  @override
  State<MinhasAplicacoes> createState() => _MinhasAplicacoesState();
}

class _MinhasAplicacoesState extends State<MinhasAplicacoes> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
        title: const Text(
          "Aplicações",
          textAlign: TextAlign.center,
        ),
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          mainAxisAlignment: getIt<GlobalConfigVars>().aplications.isEmpty
              ? MainAxisAlignment.center
              : MainAxisAlignment.start,
          children: [
            getIt<GlobalConfigVars>().aplications.isEmpty
                ? const Center(child: Text("Não criou nenhum relatório"))
                : SizedBox(
                    height: 400,
                    child: ListView.builder(
                        itemCount: getIt<GlobalConfigVars>().aplications.length,
                        itemBuilder: (context, index) {
                          return CustomCardButton(
                            title:
                                "Aplicação ${getIt<GlobalConfigVars>().aplications[index].data} ",
                            onTap: () {},
                          );
                        }),
                  )
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          context.push("/aplicacoes");
        },
        child: const Icon(
          Icons.add,
          color: Colors.white,
        ),
      ),
    );
  }
}

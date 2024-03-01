import 'package:flutter/material.dart';
import 'package:flytec/features/aplications_v2/controller/aplications_initialization_controller.dart';
import 'package:flytec/features/aplications_v2/controller/report_aplication_controller.dart';
import 'package:flytec/features/aplications_v2/pages/report_aplication_page.dart';

class AplicationsPage extends StatefulWidget {
  const AplicationsPage({super.key});

  @override
  State<AplicationsPage> createState() => _AplicationsPageState();
}

class _AplicationsPageState extends State<AplicationsPage> {
  late AplicationsInitializationController _aplicationsInitializationController;
  final ReportAplicationController _reportAplicationController =
      ReportAplicationController();

  Future<void> _initializationAplicationsReports() async {
    _aplicationsInitializationController =
        AplicationsInitializationController();
    await _aplicationsInitializationController.initialize();
    setState(() {});
  }

  @override
  void initState() {
    super.initState();
    _initializationAplicationsReports();
  }

  @override
  void dispose() {
    _reportAplicationController.dispose();
    super.dispose();
  }

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
      floatingActionButton: FloatingActionButton(
        onPressed: () async {
          await _aplicationsInitializationController.teste();
          setState(() {});
        },
        child: const Icon(Icons.add),
      ),
      body: Builder(
        builder: (context) {
          if (_aplicationsInitializationController.reportsAplications == null) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }
          if (_aplicationsInitializationController
              .reportsAplications!.isEmpty) {
            return const Center(
              child: Text('NÃO TEM ELEMENTOS'),
            );
          }
          return ListView.builder(
              itemCount: _aplicationsInitializationController
                  .reportsAplications?.length,
              itemBuilder: (context, index) {
                return InkWell(
                  onTap: () {
                    _reportAplicationController.setNewIdRelatorioAplicacoes(
                        _aplicationsInitializationController
                            .reportsAplications![index]["id"]);
                    setState(() {});
                    Navigator.push(context,
                        MaterialPageRoute(builder: (context) {
                      return const ReportAplicationPage();
                    }));
                  },
                  child: ListTile(
                    title: Text(_aplicationsInitializationController
                        .reportsAplications![index]["id"]
                        .toString()),
                  ),
                );
              });
        },
      ),
    );
  }
}

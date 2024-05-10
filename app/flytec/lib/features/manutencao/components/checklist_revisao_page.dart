import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:flytec/features/manutencao/components/widgets/custom_card_with_color_and_widget.dart';
import 'package:flytec/features/manutencao/models/revisao_model.dart';

class CheckListRevisaoPage extends StatefulWidget {
  const CheckListRevisaoPage({super.key});

  @override
  State<CheckListRevisaoPage> createState() => _CheckListRevisaoPageState();
}

class _CheckListRevisaoPageState extends State<CheckListRevisaoPage> {
  final List<RevisaoModel> _stepsRevisoes = [
    RevisaoModel(isSelected: false, title: 'Item 1'),
    RevisaoModel(isSelected: true, title: 'Item 2'),
    RevisaoModel(isSelected: false, title: 'Item 3')
  ];
  final ScrollController _scrollController = ScrollController();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: const Text('Checklist Revisão'),
          centerTitle: true,
        ),
        body: Padding(
            padding: const EdgeInsets.all(16.0),
            child: ListView(
              controller: _scrollController,
              children: [
                const Text(
                  'Aeronave PTX-123',
                  style: TextStyle(fontSize: 14, fontWeight: FontWeight.w500),
                ),
                ListView.builder(
                    controller: _scrollController,
                    shrinkWrap: true,
                    itemCount: _stepsRevisoes.length,
                    itemBuilder: (context, index) => Padding(
                          padding: const EdgeInsets.symmetric(vertical: 8.0),
                          child: Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              CustomCardWithColorAndWidget(
                                width: MediaQuery.of(context).size.width *0.2,
                                height: 70,
                                child: Center(
                                  child: Text(_stepsRevisoes[index].title!,
                                      maxLines: 2,
                                      textAlign: TextAlign.center,
                                      style: const TextStyle(
                                       
                                        fontSize: 14,
                                        fontFamily: 'Inter',
                                        fontWeight: FontWeight.w600,
                                      )),
                                ),
                              ),
                              CustomCardWithColorAndWidget(
                                width: MediaQuery.of(context).size.width *0.3,
                                height: 70,
                                child: Column(
                                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                  children: [
                                    SizedBox(
                                      height: 25,
                                      child: Checkbox(
                                          value:
                                              _stepsRevisoes[index].isSelected,
                                          onChanged: (value) {
                                            _stepsRevisoes[index]
                                                .isSelectedNewValue(value!);
                                            setState(() {});
                                          }),
                                    ),
                                    const Text('Revisão Ok',
                                        textAlign: TextAlign.center,
                                        style: TextStyle(
                                          fontSize: 12,
                                          fontFamily: 'Inter',
                                          fontWeight: FontWeight.w600,
                                        )),
                                  ],
                                ),
                              ),
                              CustomCardWithColorAndWidget(
                                width: MediaQuery.of(context).size.width *0.4,
                                height: 70,
                                child: Row(
                                  mainAxisAlignment: MainAxisAlignment.spaceAround,
                                  children: [
                                    const SizedBox(
                                      width: 70,
                                      child: Text('Realizar Manutenção',
                                          maxLines: 2,
                                          textAlign: TextAlign.center,
                                          style: TextStyle(
                                            fontSize: 12,
                                            fontFamily: 'Inter',
                                            fontWeight: FontWeight.w600,
                                          )),
                                    ),
                                    SvgPicture.asset("assets/images/arrow.svg"),
                                  ],
                                ),
                              ),
                            ],
                          ),
                        ))
              ],
            )));
  }
}

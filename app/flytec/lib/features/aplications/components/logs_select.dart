import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';
import 'package:flytec/core/utils/util.dart';
import 'package:flytec/features/aplications/components/components_exports.dart';

class LogsSelect extends StatefulWidget {
  final Function(String) onChanged;
  const LogsSelect({super.key, required this.onChanged});

  @override
  State<LogsSelect> createState() => _LogsSelectState();
}

class _LogsSelectState extends State<LogsSelect> {
  final TextEditingController _textFlightHeight = TextEditingController();

  bool get _isCountDGPSMax => getIt<GlobalConfigVars>().logs.length > 5;
  String _selectedLog = '';
  int? _selectedLogIndex;
  void _updateSelectedLog() {
    setState(() {
      _selectedLog = '';
      for (String element in getIt<GlobalConfigVars>().logs) {
        _selectedLog += '$element,';
      }
      _selectedLog = _selectedLog.substring(0, _selectedLog.length - 1);
    });
  }

  @override
  Widget build(BuildContext context) {
    return SizedBox(
        height: MediaQuery.of(context).size.height * 0.35,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            SizedBox(
              height: _isCountDGPSMax
                  ? MediaQuery.of(context).size.height * 0.25
                  : MediaQuery.of(context).size.height * 0.2,
              width: MediaQuery.of(context).size.width,
              child: ListView.builder(
                itemCount: getIt<GlobalConfigVars>().logs.length,
                shrinkWrap: true,
                padding: EdgeInsets.zero,
                itemBuilder: (context, index) => Card(
                  elevation: 0,
                  child: Align(
                      alignment: Alignment.centerLeft,
                      child: Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: Row(
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          children: [
                            SizedBox(
                              width: MediaQuery.of(context).size.width * 0.43,
                              child: Text(getIt<GlobalConfigVars>().logs[index],
                                  style: const TextStyle(
                                      color: Colors.black,
                                      fontSize: 16,
                                      fontWeight: FontWeight.w600)),
                            ),
                            InkWell(
                              child: const Icon(Icons.edit),
                              onTap: () {
                                _textFlightHeight.text =
                                    getIt<GlobalConfigVars>().logs[index];
                                _selectedLogIndex = index;
                                setState(() {});
                              },
                            ),
                            InkWell(
                              child: const Icon(Icons.delete),
                              onTap: () {
                                getIt<GlobalConfigVars>().logs.removeAt(index);
                                _updateSelectedLog();
                                widget.onChanged(_selectedLog);
                                setState(() {});
                              },
                            )
                          ],
                        ),
                      )),
                ),
              ),
            ),
            if (!_isCountDGPSMax)
              SizedBox(
                height: 45,
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    Container(
                      width: MediaQuery.of(context).size.width / 2.25,
                      alignment: Alignment.topCenter,
                      padding: const EdgeInsets.symmetric(
                          horizontal: 4.0, vertical: 0),
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
                        maxLength: 20,
                        onSubmitted: (value) {
                          if (_selectedLogIndex != null) {
                            getIt<GlobalConfigVars>().logs[_selectedLogIndex!] =
                                _textFlightHeight.text;
                            _textFlightHeight.clear();
                            _selectedLogIndex = null;
                            _updateSelectedLog();
                            widget.onChanged(_selectedLog);
                            setState(() {});
                            return;
                          }
                          getIt<GlobalConfigVars>()
                              .logs
                              .add(_textFlightHeight.text);
                          _updateSelectedLog();
                          widget.onChanged(_selectedLog);
                          setState(() {});
                        },
                        textAlign: TextAlign.start,
                        decoration: const InputDecoration(
                            hintText: "Digite um log",
                            border: InputBorder.none,
                            counterText: "",
                            hintStyle: TextStyle(
                              color: Color.fromARGB(255, 121, 118, 118),
                              fontSize: 12,
                              height: 45,
                              fontFamily: 'Inter',
                              fontWeight: FontWeight.w500,
                            )),
                      ),
                    ),
                    InkWell(
                      onTap: () {
                        if (_selectedLogIndex != null) {
                          getIt<GlobalConfigVars>().logs[_selectedLogIndex!] =
                              _textFlightHeight.text;
                          _textFlightHeight.clear();
                          _selectedLogIndex = null;
                          _updateSelectedLog();
                          widget.onChanged(_selectedLog);
                          setState(() {});
                          return;
                        }

                        getIt<GlobalConfigVars>()
                            .logs
                            .add(_textFlightHeight.text);
                        _textFlightHeight.clear();
                        _updateSelectedLog();
                        widget.onChanged(_selectedLog);
                        setState(() {});
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
            CustomButton(
              onClick: () {
                if (_textFlightHeight.text.isNotEmpty) {
                  getIt<GlobalConfigVars>().logs.add(_textFlightHeight.text);
                }
                _updateSelectedLog();
                Util.closeKeyBoard();
                widget.onChanged(_selectedLog);
                setState(() {});
                Navigator.pop(context);
              },
              title: 'ok',
            )
          ],
        ));
  }
}

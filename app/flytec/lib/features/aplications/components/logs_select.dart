import 'package:flutter/material.dart';
import 'package:flytec/core/injections/get_it.dart';
import 'package:flytec/core/utils/global_config_vars.dart';

class LogsSelect extends StatefulWidget {
  final Function(String) onChanged;
  const LogsSelect({super.key, required this.onChanged});

  @override
  State<LogsSelect> createState() => _LogsSelectState();
}

class _LogsSelectState extends State<LogsSelect> {
  final TextEditingController _textFlightHeight = TextEditingController();

  bool get _isCountDGPSMax => getIt<GlobalConfigVars>().logs.length > 6;

  @override
  Widget build(BuildContext context) {
    return SizedBox(
        height: MediaQuery.of(context).size.height * 0.285,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            SizedBox(
              height: _isCountDGPSMax
                  ? MediaQuery.of(context).size.height * 0.25
                  : MediaQuery.of(context).size.height * 0.2,
              child: ListView.builder(
                itemCount: getIt<GlobalConfigVars>().logs.length,
                shrinkWrap: true,
                padding: EdgeInsets.zero,
                itemBuilder: (context, index) => MaterialButton(
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(8.0)),
                    padding: EdgeInsets.zero,
                    color: Colors.white,
                    elevation: 0,
                    onPressed: () {
                      widget.onChanged(getIt<GlobalConfigVars>().logs[index]);
                      setState(() {});
                      Navigator.of(context).pop();
                    },
                    child: Align(
                        alignment: Alignment.centerLeft,
                        child: Padding(
                          padding: const EdgeInsets.all(8.0),
                          child: Text(getIt<GlobalConfigVars>().logs[index],
                              style: const TextStyle(
                                  color: Colors.black,
                                  fontSize: 16,
                                  fontWeight: FontWeight.w600)),
                        ))),
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
                      child: Flexible(
                        child: TextField(
                          controller: _textFlightHeight,
                          keyboardType: TextInputType.text,
                          maxLength: 20,
                          onSubmitted: (value) {
                            widget.onChanged(_textFlightHeight.text);

                            getIt<GlobalConfigVars>()
                                .logs
                                .add(_textFlightHeight.text);
                            setState(() {});
                            Navigator.of(context).pop();
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
                    ),
                    InkWell(
                      onTap: () {
                        widget.onChanged(_textFlightHeight.text);
                        getIt<GlobalConfigVars>()
                            .logs
                            .add(_textFlightHeight.text);
                        setState(() {});
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

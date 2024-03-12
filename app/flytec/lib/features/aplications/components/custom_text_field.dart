import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class CustomTextField extends StatelessWidget {
  const CustomTextField(
      {super.key,
      required this.textEditingController,
      required this.onChanged,
      this.textInputType = TextInputType.text,
      this.disabled = false,
      this.text = "Digite aqui",
      this.formater = const []});
  final TextEditingController? textEditingController;
  final TextInputType? textInputType;
  final Function(String value) onChanged;
  final bool disabled;
  final String text;
  final List<TextInputFormatter> formater;
  @override
  Widget build(BuildContext context) {
    return AbsorbPointer(
        absorbing: disabled,
        child: Container(
          width: double.infinity,
          margin: const EdgeInsets.only(bottom: 10),
          padding: const EdgeInsets.symmetric(horizontal: 16),
          decoration: ShapeDecoration(
            shape: RoundedRectangleBorder(
              side: const BorderSide(width: 1, color: Color(0xFF636363)),
              borderRadius: BorderRadius.circular(10),
            ),
          ),
          child: TextField(
            onChanged: onChanged,
            inputFormatters: formater,
            keyboardType: textInputType,
            controller: textEditingController,
            textAlign: TextAlign.left,
            textDirection: TextDirection.ltr,
            decoration: InputDecoration(
                hintText: text,
                border: InputBorder.none,
                hintStyle: const TextStyle(
                  color: Color.fromARGB(255, 121, 118, 118),
                  fontSize: 16,
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w500,
                )),
          ),
        ));
  }
}

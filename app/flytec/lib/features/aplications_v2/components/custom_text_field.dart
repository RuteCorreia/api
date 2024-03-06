import 'package:flutter/material.dart';

class CustomTextField extends StatelessWidget {
  const CustomTextField(
      {super.key,
      required this.textEditingController,
      required this.onChanged,
      this.textInputType = TextInputType.text});
  final TextEditingController? textEditingController;
  final TextInputType? textInputType;
  final Function(String value) onChanged;
  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: 50,
      margin: const EdgeInsets.only(bottom: 20),
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 10),
      decoration: ShapeDecoration(
        shape: RoundedRectangleBorder(
          side: const BorderSide(width: 1, color: Color(0xFF636363)),
          borderRadius: BorderRadius.circular(10),
        ),
      ),
      child: TextField(
        onChanged: onChanged,
        keyboardType: textInputType,
        controller: textEditingController,
        decoration: const InputDecoration(
            hintText: "Digite aqui",
            border: InputBorder.none,
            hintStyle: TextStyle(
              color: Color.fromARGB(255, 121, 118, 118),
              fontSize: 16,
              fontFamily: 'Inter',
              fontWeight: FontWeight.w500,
              height: 0.09,
            )),
      ),
    );
  }
}

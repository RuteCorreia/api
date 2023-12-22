// ignore_for_file: must_be_immutable

import 'package:flutter/material.dart';

class RecoverPassWordButton extends StatelessWidget {
  RecoverPassWordButton({super.key, required this.onClick});

  VoidCallback? onClick;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onClick,
      child: Container(
        width: 210,
        height: 40,
        padding: const EdgeInsets.only(
          top: 10,
          left: 24,
          right: 20,
          bottom: 10,
        ),
        decoration: ShapeDecoration(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(10),
          ),
        ),
        child: const Row(
          mainAxisSize: MainAxisSize.min,
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Text(
              'ESQUECI MINHA SENHA',
              style: TextStyle(
                color: Color(0xFF00B45D),
                fontSize: 14,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w600,
                decoration: TextDecoration.underline,
                height: 0.11,
              ),
            ),
          ],
        ),
      ),
    );
  }
}

import 'package:flutter/material.dart';

class CustomCardWithColorAndWidget extends StatelessWidget {
  final double height;
  final double width;
  final Widget? child;
  final VoidCallback? onTap;
  const CustomCardWithColorAndWidget(
      {super.key,
      required this.child,
      this.height = 50,
      this.onTap,
      this.width = 328});

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Container(
          width: width,
          height: height,
          padding: const EdgeInsets.all(10),
          decoration: ShapeDecoration(
            color: Colors.white,
            shape: RoundedRectangleBorder(
              side: const BorderSide(width: 2, color: Color(0xFF00B45D)),
              borderRadius: BorderRadius.circular(8),
            ),
          ),
          child: child),
    );
  }
}

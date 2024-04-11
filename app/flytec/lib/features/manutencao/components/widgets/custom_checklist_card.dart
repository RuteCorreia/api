import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CustomCardWithColor extends StatelessWidget {
  final String? title;
  final bool? isSelected;
  final double height;
  final VoidCallback? onTap;
  const CustomCardWithColor(
      {super.key, required this.title, this.isSelected = false, this.height = 50, this.onTap});

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Container(
        width: 328,
        height: height,
        padding: const EdgeInsets.all(10),
        decoration: ShapeDecoration(
          color: Colors.white,
          shape: RoundedRectangleBorder(
            side: const BorderSide(width: 2, color: Color(0xFF00B45D)),
            borderRadius: BorderRadius.circular(8),
          ),
        ),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Text(title!,
                maxLines: 2,
                textAlign: TextAlign.center,
                style: const TextStyle(
                  color: Color.fromARGB(255, 121, 118, 118),
                  fontSize: 16,
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w600,
                )),
            const SizedBox(width: 16),
            SvgPicture.asset("assets/images/arrow.svg"),
          ],
        ),
      ),
    );
  }
}

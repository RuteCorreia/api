import 'package:flutter/material.dart';

class ActivityButton extends StatelessWidget {
  const ActivityButton({
    super.key,
    required this.text,
    required this.value,
    required this.onTap,
  });
  final String? value;
  final String? text;
  final VoidCallback? onTap;
  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Container(
        height: 135,
        padding: const EdgeInsets.all(8.0),
        clipBehavior: Clip.antiAlias,
        decoration: ShapeDecoration(
          shape: RoundedRectangleBorder(
            side: const BorderSide(width: 2, color: Color(0xFF00B45D)),
            borderRadius: BorderRadius.circular(8),
          ),
        ),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              value!,
              maxLines: 2,
              style: const TextStyle(
                color: Color.fromARGB(255, 121, 118, 118),
                fontSize: 32,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w700,
              ),
            ),
            const SizedBox(height: 10),
            Text(
              text!,
              maxLines: 2,
              style: const TextStyle(
                color: Color.fromARGB(255, 121, 118, 118),
                fontSize: 14,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w700,
              ),
            ),
          ],
        ),
      ),
    );
  }
}

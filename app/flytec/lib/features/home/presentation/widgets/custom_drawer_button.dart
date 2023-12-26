import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CustomDrawerButton extends StatelessWidget {
  const CustomDrawerButton({
    super.key,
    required this.imageUrl,
    required this.onClick,
    required this.text,
  });

  final String? text;
  final String? imageUrl;
  final VoidCallback? onClick;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onClick,
      child: SizedBox(
        width: 189,
        height: 24,
        child: Row(
          mainAxisSize: MainAxisSize.min,
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Container(
              width: 24,
              height: 24,
              clipBehavior: Clip.antiAlias,
              decoration: const BoxDecoration(),
              child: Row(
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  SizedBox(
                    width: 24,
                    height: 24,
                    child: Stack(children: [
                      SvgPicture.asset(
                        imageUrl!,
                      )
                    ]),
                  ),
                ],
              ),
            ),
            const SizedBox(width: 19),
            Text(
              text!,
              style: const TextStyle(
                color: Color.fromARGB(255, 121, 118, 118),
                fontSize: 14,
                fontFamily: 'Inter',
                fontWeight: FontWeight.w600,
                height: 0.11,
              ),
            ),
          ],
        ),
      ),
    );
  }
}

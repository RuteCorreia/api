import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CustomDrawerButton extends StatelessWidget {
  const CustomDrawerButton({
    super.key,
    this.imageUrl,
    required this.onClick,
    required this.text,
    this.icon,
  });

  final String? text;
  final String? imageUrl;
  final VoidCallback? onClick;
  final IconData? icon;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onClick,
      child: Padding(
        padding: const EdgeInsets.symmetric(vertical: 8.0),
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
                        if (imageUrl != null)
                          SvgPicture.asset(
                            imageUrl!,
                          ),
                        if (icon != null)
                          Icon(
                            icon,
                            color: Colors.green,
                          ),
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
      ),
    );
  }
}

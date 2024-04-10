import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

class CustomMenuButton extends StatelessWidget {
  const CustomMenuButton({super.key, required this.onClick});

  final VoidCallback? onClick;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onClick,
      child: SvgPicture.asset(
        "assets/images/group.svg",
        fit: BoxFit.none,
      ),
    );
  }
}

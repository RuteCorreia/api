import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

class CustomMenuButton extends StatelessWidget {
  const CustomMenuButton({super.key, required this.onClick});

  final VoidCallback? onClick;

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onClick,
      child: Container(
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
              child: Stack(
                  children: [SvgPicture.asset("assets/images/group.svg")]),
            ),
          ],
        ),
      ),
    );
  }
}

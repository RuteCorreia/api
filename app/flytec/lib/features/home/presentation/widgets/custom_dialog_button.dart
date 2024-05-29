import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CustomDialogButton extends StatelessWidget {
  const CustomDialogButton(
      {super.key,
      required this.onClick,
      this.leftIcon,
      required this.text,
      this.icon = Icons.add,
      this.showLeftIcon = true,
      this.showRightcon = true});
  final String? leftIcon;
  final VoidCallback? onClick;
  final String? text;
  final bool? showLeftIcon;
  final bool? showRightcon;
  final IconData? icon;
  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onClick,
      child: Container(
        width: 300,
        height: 91,
        padding: const EdgeInsets.all(16),
        clipBehavior: Clip.antiAlias,
        decoration: ShapeDecoration(
          color: Colors.white,
          shape: RoundedRectangleBorder(
            side: const BorderSide(width: 1, color: Color(0xFFECEAEA)),
            borderRadius: BorderRadius.circular(8),
          ),
        ),
        child: Row(
          mainAxisSize: MainAxisSize.min,
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            showLeftIcon!
                ? Container(
                    width: 24,
                    height: 24,
                    clipBehavior: Clip.antiAlias,
                    decoration: const BoxDecoration(),
                    child: Stack(children: [
                      leftIcon != null
                          ? SvgPicture.asset(leftIcon!)
                          : Icon(
                              icon,
                              color: Colors.green,
                            )
                    ]),
                  )
                : const SizedBox(),
            const SizedBox(width: 8),
            Expanded(
              child: Column(
                mainAxisSize: MainAxisSize.min,
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  SizedBox(
                    width: double.infinity,
                    child: Text(
                      text!,
                      style: const TextStyle(
                        color: Color.fromARGB(255, 121, 118, 118),
                        fontSize: 13,
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w600,
                      ),
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(width: 8),
            showRightcon!
                ? Container(
                    width: 24,
                    height: 24,
                    clipBehavior: Clip.antiAlias,
                    decoration: const BoxDecoration(),
                    child: Stack(
                      children: [
                        SvgPicture.asset("assets/images/icone.svg"),
                      ],
                    ),
                  )
                : const SizedBox(),
          ],
        ),
      ),
    );
  }
}

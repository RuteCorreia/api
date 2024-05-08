extension DateTimeExtension on DateTime {
  String to24hours() {
    final hourFormatter = hour.toString().padLeft(2, "0");
    final minFormatter = minute.toString().padLeft(2, "0");
    return "$hourFormatter:$minFormatter";
  }
}

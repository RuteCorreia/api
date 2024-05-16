extension EnumExtension on Enum {
  String get toName {
    return toString().split('.').last;
  }
}
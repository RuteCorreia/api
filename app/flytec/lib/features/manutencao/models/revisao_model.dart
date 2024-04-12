class RevisaoModel{
  final String? title;
   bool? isSelected;

  RevisaoModel({this.title, this.isSelected = false});

  void isSelectedNewValue(bool? newValue) => isSelected = newValue;
}
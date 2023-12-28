import 'package:flutter/material.dart';

class ProductTypeSelect extends StatelessWidget {
  final Function(String) onChangedProductType;
  ProductTypeSelect({super.key, required this.onChangedProductType});

  final TextEditingController _textProduct = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return SizedBox(
        height: MediaQuery.of(context).size.height * 0.2,
        child: Column(children: [
          MaterialButton(
              shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(8.0)),
              padding: EdgeInsets.zero,
              color: Colors.white,
              elevation: 0,
              onPressed: () {
                onChangedProductType('Sólido');
                Navigator.of(context).pop();
              },
              child: const Align(
                  alignment: Alignment.centerLeft,
                  child: Padding(
                    padding: EdgeInsets.all(8.0),
                    child: Text("Sólido",
                        style: TextStyle(
                            color: Colors.black,
                            fontSize: 16,
                            fontWeight: FontWeight.w600)),
                  ))),
          MaterialButton(
              shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(8.0)),
              padding: EdgeInsets.zero,
              color: Colors.white,
              elevation: 0,
              onPressed: () {
                onChangedProductType('Líquido');
                Navigator.of(context).pop();
              },
              child: const Align(
                  alignment: Alignment.centerLeft,
                  child: Padding(
                    padding: EdgeInsets.all(8.0),
                    child: Text("Líquido",
                        style: TextStyle(
                            color: Colors.black,
                            fontSize: 16,
                            fontWeight: FontWeight.w600)),
                  ))),
          const SizedBox(height: 14),
          SizedBox(
            height: 40,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Container(
                  width: MediaQuery.of(context).size.width / 2.1,
                  alignment: Alignment.topCenter,
                  padding: const EdgeInsets.symmetric(horizontal: 8.0),
                  decoration: ShapeDecoration(
                    shape: RoundedRectangleBorder(
                      side:
                          const BorderSide(width: 1, color: Color(0xFF636363)),
                      borderRadius: BorderRadius.circular(10),
                    ),
                  ),
                  child: TextField(
                    controller: _textProduct,
                    keyboardType: TextInputType.number,
                    onSubmitted: (value) {
                      onChangedProductType(_textProduct.text);
                      Navigator.of(context).pop();
                    },
                    decoration: const InputDecoration(
                        hintText: "Digite um produto",
                        border: InputBorder.none,
                        hintStyle: TextStyle(
                          color: Color.fromARGB(255, 121, 118, 118),
                          fontSize: 14,
                          fontFamily: 'Inter',
                          fontWeight: FontWeight.w500,
                        )),
                  ),
                ),
                InkWell(
                  onTap: () {
                    onChangedProductType(_textProduct.text);
                    Navigator.of(context).pop();
                  },
                  child: Container(
                    width: MediaQuery.of(context).size.width * 0.14,
                    height: MediaQuery.of(context).size.width * 0.12,
                    padding: const EdgeInsets.all(1),
                    clipBehavior: Clip.antiAlias,
                    decoration: ShapeDecoration(
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(8.0),
                      ),
                      color: Colors.green,
                    ),
                    child: const Column(
                      mainAxisSize: MainAxisSize.min,
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Icon(
                          Icons.check,
                          color: Colors.white,
                        )
                      ],
                    ),
                  ),
                ),
              ],
            ),
          ),
        ]));
  }
}

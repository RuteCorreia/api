import 'dart:collection';
import 'dart:io';

import 'package:flutter/foundation.dart';

class FileManager {
  static final Queue<String> buffer = Queue();
  static bool saving = false;
  static Future<void> writeToLogFile(String log) async {
    buffer.add(log);
    if (!saving) {
      try {
        saveBufferToFile();
      } catch (e) {
        saving = false;
      }
    }
  }

  static Future<String> readLogFile() async {
    final file = await _getTempLogFile();
    return file.readAsString();
  }

  static Future<File> _getTempLogFile() async {
    final path = await getFullPath('trklog');
    final file = File(path);
    if (!await file.exists()) {
      await file.writeAsString('');
    }
    return file;
  }

  static Future<void> clearLogFile() async {
    final file = await _getTempLogFile();
    await file.writeAsString('');
  }

  static Future<File> rename(String filename) async {
    final file = await _getTempLogFile();
    final newPath = await getFullPath(filename);
    final newFile = File(newPath);
    if (await newFile.exists()) {
      newFile.delete();
    }
    await file.rename(newPath);
    return file;
  }

  static Future<bool> fileExists(filename) async {
    final path = await getFullPath(filename);
    final file = File(path);
    return file.exists();
  }

  static Future<String> readFile(fileName) async {
    final path = await getFullPath(fileName);
    final file = File(path);
    if (!await file.exists()) {
      if (kDebugMode) {
        print("Arquivo não existe! $fileName");
      }
      return "";
    } else {
      return file.readAsString();
    }
  }

  static getFullPath(fileName) async {
    try {
      final directory = Directory.systemTemp;
      return '${directory.path}/$fileName.txt';
    } catch (e) {
      if (kDebugMode) {
        print(e);
      }
      rethrow;
    }
  }

  static void saveBufferToFile() {
    saving = true;
    _getTempLogFile().then((file) {
      var reg = buffer.removeFirst();
      file.writeAsString('$reg,', mode: FileMode.append).then((value) {
        if (kDebugMode) {
          print("SAVING: $reg");
        }
        if (buffer.isNotEmpty) {
          saveBufferToFile();
        } else {
          saving = false;
        }
      });
    });
  }
}

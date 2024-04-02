import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';

abstract class SignatureRepository {
  Future<Either<Failure, void>> saveSignature(String imageEncoded);
  Future<Either<Failure, String>> getSignature();
}

import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/signature/data/datasources/remote_signture_datasource.dart';
import 'package:flytec/features/signature/domain/repositories/signature_repository.dart';

class SignatureRepositoryImpl implements SignatureRepository {
  final RemoteSignatureDataSourceImpl remoteSignatureDataSourceImpl;

  SignatureRepositoryImpl({
    required this.remoteSignatureDataSourceImpl,
  });

  @override
  Future<Either<Failure, String>> getSignature() async {
    try {
      final result = await remoteSignatureDataSourceImpl.getSignature();
      return Right(result);
    } on ServerException {
      return Left(ServerFailure(message: "Ocorreu um erro  no servidor"));
    } on LoginException {
      return Left(LoginFailure());
    } on NetWorkException {
      return Left(NetWorkFailure());
    }
  }

  @override
  Future<Either<Failure, void>> saveSignature(String imageEncoded) async {
    try {
      final result =
          await remoteSignatureDataSourceImpl.saveSignature(imageEncoded);
      return Right(result);
    } on ServerException {
      return Left(ServerFailure(message: "Ocorreu um erro  no servidor"));
    } on LoginException {
      return Left(LoginFailure());
    } on NetWorkException {
      return Left(NetWorkFailure());
    }
  }
}

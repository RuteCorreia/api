import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/executor/domain/repositories/executor_repository.dart';
import 'package:flytec/features/signature/domain/repositories/signature_repository.dart';

class GetSignatureUseCase extends UseCase<String, NoParams> {
  final SignatureRepository? _signatureRepository;
  GetSignatureUseCase(this._signatureRepository);

  @override
  Future<Either<Failure, String>> call(NoParams? params) async {
    final result = await _signatureRepository!.getSignature();
    return result;
  }
}

import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/signature/domain/repositories/signature_repository.dart';

class SaveSignatureUseCase extends UseCase<void, String> {
  final SignatureRepository? _signatureRepository;
  SaveSignatureUseCase(this._signatureRepository);

  @override
  Future<Either<Failure, void>> call(String? params) async {
    return await _signatureRepository!.saveSignature(params!);
  }
}

import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/auth/domain/repositories/company_repository.dart';

class ObtainLogoCompanyUseCase extends UseCase<String?, String> {
  final ICompanyRepository? _companyRepository;
  ObtainLogoCompanyUseCase(this._companyRepository);

  @override
  Future<Either<Failure, String?>> call(String? params) async =>
      await _companyRepository!.obtainLogoCompany(companyId: params);
}

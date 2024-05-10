import 'package:dartz/dartz.dart';
import 'package:flytec/features/auth/data/datasources/remote_company_data_source.dart';
import 'package:flytec/features/auth/domain/repositories/company_repository.dart';

import '../../../../core/errors/exception.dart';
import '../../../../core/errors/failures.dart';

class CompanyRepositoryImpl implements ICompanyRepository {
  final RemoteCompanyDataSourceImpl remoteCompanyDataSourceImpl;

  CompanyRepositoryImpl({
    required this.remoteCompanyDataSourceImpl,
  });

  @override
  Future<Either<Failure, String>> obtainLogoCompany({String? companyId}) async {
    try {
      if (companyId == null) return const Right('');
      final userData =
          await remoteCompanyDataSourceImpl.obtainLogoCompany(companyId);
      return Right(userData ?? '');
    } on ServerException {
      return Left(ServerFailure(
          message: "Ocorreu um erro ao pegar as informações da empresa"));
    } on LoginException {
      return Left(LoginFailure());
    } on NetWorkException {
      return Left(NetWorkFailure());
    }
  }
}

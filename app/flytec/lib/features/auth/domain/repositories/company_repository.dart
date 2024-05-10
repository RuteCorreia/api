import 'package:dartz/dartz.dart';

import '../../../../core/errors/failures.dart';

abstract class ICompanyRepository {
  Future<Either<Failure, String?>> obtainLogoCompany({
    String? companyId,
  });
}

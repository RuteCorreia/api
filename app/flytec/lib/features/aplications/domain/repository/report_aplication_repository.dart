import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/aplications/domain/entities/report_aplications_entity.dart';

abstract class ReportAplicationRepository {
  Future<Either<Failure, bool>> sendReportAplication(ReportAplicationEntity? reportAplicationsModel);
}

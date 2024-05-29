import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/core/usecase/usecase.dart';
import 'package:flytec/features/aplications/domain/entities/report_aplications_entity.dart';
import 'package:flytec/features/aplications/domain/repository/report_aplication_repository.dart';

class SendReportAplicationUseCase
    extends UseCase<bool, ReportAplicationEntity?> {
  final ReportAplicationRepository? _repository;
  SendReportAplicationUseCase(this._repository);

  @override
  Future<Either<Failure, bool>> call(
      ReportAplicationEntity? params) async {
    final result = await _repository!.sendReportAplication(params);
    return result;
  }
}

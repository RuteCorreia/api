import 'package:dartz/dartz.dart';
import 'package:flytec/core/errors/exception.dart';
import 'package:flytec/core/errors/failures.dart';
import 'package:flytec/features/aplications/data/datasource/remote_report_aplications_datasource.dart';
import 'package:flytec/features/aplications/data/models/report_aplications_model.dart';
import 'package:flytec/features/aplications/domain/entities/report_aplications_entity.dart';
import 'package:flytec/features/aplications/domain/repository/report_aplication_repository.dart';

class ReportAplicationRepositoryImpl implements ReportAplicationRepository {
  final RemoteReportAplicationsDatasource remoteReportAplicationsDatasource;

  ReportAplicationRepositoryImpl({
    required this.remoteReportAplicationsDatasource,
  });

  @override
  Future<Either<Failure, bool>> sendReportAplication(
      ReportAplicationEntity? reportAplicationsModel) async {
    try {
      final result =
          await remoteReportAplicationsDatasource.sendReportAplication(
              reportAplicationsModel as ReportAplicationsModel?);
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

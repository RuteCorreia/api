class SQLCommands {
  static String createAplicacaoTable = '''
CREATE TABLE Aplicacao (
    id INTEGER PRIMARY KEY,
    piloto TEXT,
    executor TEXT,
    state INTEGER,
    data TEXT,
    contratante_id INTEGER,
    identificacaoAreaTratada_id INTEGER,
    caracteristicasProdutoAplicado_id INTEGER,
    recomendacoesTecnicas_id INTEGER,
    relatorioAplicacao_id INTEGER,
    contratoPrestacaoServico_id INTEGER,
    dadosResponsavel_id INTEGER,
    refUsuario TEXT
)
''';
  static String createContratanteTable = '''
CREATE TABLE Contratante (
    id INTEGER PRIMARY KEY,
    tipoContratante TEXT,
    nome TEXT,
    cpf TEXT,
    endereco TEXT,
    rg TEXT,
    uf TEXT,
    cidade TEXT,
    cnpj TEXT,
    inscricaoEstadual TEXT,
    contratanteRef TEXT
)
''';
  static String createIdentificacaoAreaTratadaTable = '''
CREATE TABLE IdentificacaoAreaTratada (
    id INTEGER PRIMARY KEY,
    uf TEXT,
    cidade TEXT,
    localizacao TEXT,
    cultura TEXT,
    extensao TEXT,
    croquiArea BLOB
)
''';
  static String createCaracteristicasProdutoAplicadoTable = '''
CREATE TABLE CaracteristicasProdutoAplicado (
    id INTEGER PRIMARY KEY,
    cultura TEXT,
    receiturarioAgronomico BLOB,
    nomeProduto TEXT,
    classificacaoToxicologica TEXT,
    classe TEXT,
    tipoFormulacao TEXT,
    alvoBiologico TEXT,
    doseProdutoHectare TEXT,
    unidadeDoseProdutoHectare TEXT,
    adjuvante TEXT,
    tipoServico TEXT,
    numeroReceituarioAgronomico TEXT,
    dataEmissao TEXT
)
''';
  static String createRecomendacoesTecnicasTable = '''
CREATE TABLE RecomendacoesTecnicas (
    id INTEGER PRIMARY KEY,
    veiculante TEXT,
    qtdVeiculante TEXT,
    larguraFaixa TEXT,
    volumeAplicacao TEXT,
    unidadevolumeAplicacao TEXT,
    aeronave TEXT,
    alturaVoo TEXT,
    temperatura TEXT,
    umidadeRelativaAr TEXT,
    velocidadeVento TEXT,
    tipoProduto TEXT,
    equipamento TEXT,
    angulo TEXT
)
''';
  static String createAplicacoesTable = '''
CREATE TABLE Aplicacoes (
    id INTEGER PRIMARY KEY,
    dataAplicacao TEXT,
    horaInicio TEXT,
    horaFinal TEXT,
    horimetroInicial TEXT,
    horimetroFinal TEXT,
    imagemCondicaoClimatica BLOB,
    temperaturaInicial TEXT,
    temperaturaFinal TEXT,
    umidadeRelativaArInicial TEXT,
    umidadeRelativaArFinal TEXT,
    ventoInicial TEXT,
    ventoFinal TEXT,
    relatorioAplicacaoId INTEGER,
    FOREIGN KEY (relatorioAplicacaoId) REFERENCES RelatorioAplicacao(id)
)
''';

  static String createRelatorioAplicacaoTable = '''
CREATE TABLE RelatorioAplicacao (
    id INTEGER PRIMARY KEY,
    cultura TEXT,
    produtoAplicado TEXT,
    dosagem TEXT,
    unidadeDosagem TEXT,
    volumeAplicacao TEXT,
    unidadeVolumeAplicacao TEXT,
    totalAreaAplicada TEXT,
    localizacaoPistaCodigoICAO TEXT,
    lat TEXT,
    long TEXT,
    densidade TEXT,
    observacoes TEXT,
    relatorioDGPS TEXT
)
''';
  static String createContratoPrestacaoServicoTable = '''
CREATE TABLE ContratoPrestacaoServico (
    id INTEGER PRIMARY KEY,
    distanciaPista TEXT,
    preco TEXT,
    unidadePreco TEXT,
    extensao TEXT,
    valorTotal TEXT,
    vencimento TEXT,
    nomePiloto TEXT,
    executor TEXT
)
''';
  static String createDadosResponsavelTable = '''
CREATE TABLE DadosResponsavel (
    id INTEGER PRIMARY KEY,
    data TEXT,
    uf TEXT,
    cidade TEXT,
    nomeCompleto TEXT,
    documento TEXT,
    telefone TEXT,
    assinaturaResponsavel BLOB
)
''';

  static String createFirefightingTable = '''
CREATE TABLE Firefighting (
    id INTEGER PRIMARY KEY,
    numeroAviso INTEGER,
    prefixoAeronave TEXT,
    state INTEGER,
    piloto TEXT,
    executor TEXT,
    uf TEXT,
    cidade TEXT,
    data INTEGER,
    horimetroAcionamento TEXT,
    pista_id INTEGER,
    localIncendio_id INTEGER,
    observacao TEXT,
    horarioFinalOperacao INTEGER,
    horimetroFinalOperacao TEXT,
    horarioCorte INTEGER,
    horimetroCorte TEXT,
    capacidadeCargaAeronave TEXT,
    totalAguaUtilizadaOperacao TEXT,
    coordenadorBaseOperacional_id INTEGER,
    comandanteOcorrencia_id INTEGER,
    cliente TEXT,
    refId TEXT
)
''';

  static String createPistaFirefightingTable = '''
CREATE TABLE PistaFirefighting (
    id INTEGER PRIMARY KEY,
    horarioChegadaPista INTEGER,
    horimetroChegadaPista TEXT,
    codigoICAOPista TEXT,
    nomePista TEXT,
    latPista TEXT,
    longPista TEXT
)
''';

  static String createLocalFirefightingTable = '''
CREATE TABLE LocalFirefighting (
    id INTEGER PRIMARY KEY,
    lat TEXT,
    long TEXT,
    referencia TEXT
)
''';
  static String createDecolagemPousoFirefightingTable = '''
CREATE TABLE DecolagemPousoFirefighting (
    id INTEGER PRIMARY KEY,
    horarioDecolagem INTEGER,
    horarioPouso INTEGER,
    horimetroDecolagem TEXT,
    horimetroPouso TEXT,
    firefightingId INTEGER,
    FOREIGN KEY (firefightingId) REFERENCES Firefighting(id)
)
''';

  static String createCoordenadorBaseOperacionalFirefightingTable = '''
CREATE TABLE CoordenadorBaseOperacionalFirefighting (
    id INTEGER PRIMARY KEY,
    nome TEXT,
    postoGraduacao TEXT,
    re TEXT,
    assinatura TEXT
)
''';
  static String createComandanteOcorrenciaFirefightingTable = '''
CREATE TABLE ComandanteOcorrenciaFirefighting (
    id INTEGER PRIMARY KEY,
    nome TEXT,
    postoGraduacao TEXT,
    re TEXT,
    assinatura TEXT
)
''';
}

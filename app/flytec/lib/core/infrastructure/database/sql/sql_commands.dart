class SQLCommands {
  static String createRelatorioAplicacaoTable = '''
  CREATE TABLE RelatorioAplicacoes (
    id INTEGER PRIMARY KEY,
    piloto TEXT,
    executor TEXT,
    data TEXT,
    identificadorRelatorio INTEGER UNIQUE,
    identificacaoContratanteId INTEGER UNIQUE,
    identificacaoAreaId INTEGER UNIQUE,
    caracteristicasProdutoId INTEGER UNIQUE,
    aplicacaoRelatorioId INTEGER UNIQUE,
    contratoPrestacaoServicosId INTEGER UNIQUE,
    dadosResponsavelId INTEGER UNIQUE
  )
''';
  static String createIdentificacaoContratanteTable = '''
  CREATE TABLE IdentificacaoContratante (
    id INTEGER PRIMARY KEY,
    nomeContratante TEXT,
    endereco TEXT,
    documento TEXT UNIQUE,
    tipoContratante TEXT,
    uf TEXT,
    cidade TEXT
  )
''';
  static String createIdentificacaoAreaTable = '''
  CREATE TABLE IdentificacaoArea (
    id INTEGER PRIMARY KEY,
    uf TEXT
    cidade TEXT,
    localizacao TEXT,
    cultura TEXT,
    extensao TEXT,
    fotoArea BLOB UNIQUE
  )
''';
  static String createCaracteristicasProdutoTable = '''
  CREATE TABLE CaracteristicasProduto (
    id INTEGER PRIMARY KEY,
    cultura TEXT,
    fotoReceituarioAgronomico BLOB UNIQUE,
    nomeProduto TEXT,
    classificacaoToxicologica TEXT,
    classe TEXT,
    tipoFormulacao TEXT,
    alvoBiologico TEXT,
    doseProdutoHectare TEXT,
    unidadeDoseProdutoHectare TEXT,
    adjuvante TEXT,
    tipoServico TEXT
  )
''';
  static String createAplicacaoTable = '''
  CREATE TABLE Aplicacao (
    id INTEGER PRIMARY KEY,
    dataAplicacao TEXT,
    horarioInicio TEXT,
    horimetroInicial TEXT,
    horarioTermino TEXT,
    horimetroFinal TEXT,
    imagemCondicoesClimaticas BLOB UNIQUE,
    temperaturaInicial INTEGER,
    temperaturaFinal INTEGER,
    urArInicial INTEGER,
    urArFinal INTEGER,
    ventoInicial INTEGER,
    ventoFinal INTEGER
  )
''';
  static String createContratoPrestacaoServicosTable = '''
  CREATE TABLE ContratoPrestacaoServicos (
    id INTEGER PRIMARY KEY,
    distanciaPista INTEGER,
    preco INTEGER,
    precoUnidade TEXT,
    extensaoHorasHa TEXT,
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
    documento TEXT UNIQUE,
    telefone TEXT,
    assinaturaResponsavel BLOB
  )
''';
}

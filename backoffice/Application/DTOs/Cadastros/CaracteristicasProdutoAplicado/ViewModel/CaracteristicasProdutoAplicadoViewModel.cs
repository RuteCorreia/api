namespace Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;

public class CaracteristicasProdutoAplicadoViewModel
{
    public int Id { get; set; }
    public string Cultura { get; set; }
    public string ReceiturarioAgronomico { get; set; }
    public string NomeProduto { get; set; }
    public int? ClassificacaoToxicologica { get; set; }
    public string Classe { get; set; }
    public string TipoFormulacao { get; set; }
    public string AlvoBiologico { get; set; }
    public string DoseProdutoHectare { get; set; }
    public string UnidadeDoseProdutoHectare { get; set; }
    public string Adjuvante { get; set; }
    public string TipoServico { get; set; }
    public string NumeroReceituarioAgronomico { get; set; }
    public string DataEmissao { get; set; }
    public bool? IsReceituarioImage { get; set; }
}

using System.Text.Json.Serialization;

namespace Application.DTOs.Cadastros.Produto.ViewModel;

public class ClasseViewModel
{
    public string? Classe { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int? IdTipoDeServico { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int? CampoAdiconado { get; set; }
}

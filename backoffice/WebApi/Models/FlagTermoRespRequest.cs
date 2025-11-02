using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class FlagTermoRespRequest
{
    [Required]
    public string FlagTermoResp { get; set; } = string.Empty;
}

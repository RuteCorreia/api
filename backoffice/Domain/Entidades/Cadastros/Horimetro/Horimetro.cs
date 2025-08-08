using Domain.Enums;

namespace Domain.Entidades.Cadastros.Horimetro
{
    public class Horimetro
    {
        public double Inicio { get; set; }
        public double Fim { get; set; }
        public HorimetroTypeEnum Tipo { get; set; }
    }
}

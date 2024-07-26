namespace Application.DTOs.Users.ViewModel
{
    public class UserProfileViewModel
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int? IdEmpresa { get; set; }
    }
}

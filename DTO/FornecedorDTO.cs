using System.ComponentModel.DataAnnotations;
namespace newmarket.DTO
{
    public class FornecedorDTO
    {
        [Required]
        public int Id { get; set;}
        [Required(ErrorMessage="Nome de fornecedor � obrigat�rio!")]
        [StringLength(100, ErrorMessage = "Nome de fornecedor muito grande, tente um nome menor!")]
        [MinLength(2, ErrorMessage = "Nome de fornecedor muito pequeno, tente um nome com mais de 2 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "E-mail de fornecedor � obrigatorio!")]
        [EmailAddress(ErrorMessage = "E-mail inv�lido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "E-mail de fornecedor � obrigatorio!")]
        [Phone(ErrorMessage = "N�mero de telefone inv�lido!")]
        public string Telefone { get; set; }
    }
}
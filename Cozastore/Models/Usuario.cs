using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Cozastore.Models;

[Table("Usuario")]
public class Usuario
{
    [Key]
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public IdentityUser ContaUsuario { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Data de Nascimento")]
    [Required(ErrorMessage = "Por favor, informe a Data de Nascimento")]
    public DateTime DataNascimento { get; set; } 

    [StringLength(300)]
    public string Foto { get; set; }
}
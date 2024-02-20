using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozastore.Models;

[Table("Cor")]
public class Cor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    [Required(ErrorMessage = "Por favor, informe o Nome")]
    [StringLength(30, ErrorMessage = "O Nome deve possui no máximo 30 caracteres")]
    public int Id  { get; set; }
    public string Nome { get; set; }

    [Display(Name = "Código Hexa", Prompt = "Ex. #000000")]
    [Required(ErrorMessage = "Por favor, informe o Código Hexa")]
    [StringLength(30, ErrorMessage = "O Código Hexa deve possui no máximo 7 caracteres")]
    public string CodigoHexa { get; set; }
}

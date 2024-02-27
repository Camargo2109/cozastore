using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cozastore.Models;

[Table("Categoria")]
public class Categoria
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id  { get; set; }

    [Required(ErrorMessage = "Por favor, informe o Nome")]
    [StringLength(30, ErrorMessage = "O Nome deve possui no máximo 30 caracteres")]
    public string Nome { get; set; }

    [StringLength(300)]
    public string Foto { get; set; }

    [Display(Name = "Exibir com filtro?")]
    public bool Filtrar {get; set; }
    
    [Display(Name = "Exibir com Banner?")]
    public bool Banner { get; set; }
    [Display(Name = "Categoria Mãe")]
    public int? CatergoriaMaeId { get; set; }
    [ForeignKey("CategoriaMaeId")]
    public Categoria CategoriaMae { get; set; }
}

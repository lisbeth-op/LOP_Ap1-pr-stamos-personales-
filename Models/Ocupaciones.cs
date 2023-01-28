using System.ComponentModel.DataAnnotations;
namespace Models;
public class Ocupaciones
{
    [Key]
    public int OcupacionId { get; set; }
    [Required(ErrorMessage = "Descripcion requerida")]
    public string? Descripcion { get; set; }
    public double Salario {get; set;}


}
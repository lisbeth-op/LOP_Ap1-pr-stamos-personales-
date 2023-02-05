using System.ComponentModel.DataAnnotations;
namespace Models;
//PersonaId,Nombres,Telefono, Celular, Email, Direccion, FechaNacimiento, OcupacionId
public class Personas
{
    [Key]
    public int PersonaId { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    [MinLength(2, ErrorMessage = "El Nombre debe tener al menos {1} Caracter.")]
    [MaxLength(15, ErrorMessage = "El Nombre no debe pasar de {2} caracter")]
    public string? Nombre { get; set; }
    [Required(ErrorMessage = "Campo Telefono es obligatorio")]
    public string? Telefono { get; set; }
    [Required(ErrorMessage = "Campo Celular obligatorio")]
    public string? Celular { get; set; }
    [Required(ErrorMessage = "Campo Email es obligatorio")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Campo Direccion es obligatorio")]
    public string? Direccion { get; set; }
    [Required(ErrorMessage = "Campo Fecha de nacimiento obligatorio")]
    public DateTime FechaNacimiento { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    public int OcupacionId { get; set; }
}
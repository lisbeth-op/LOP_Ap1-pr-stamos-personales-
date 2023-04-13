// PrestamoId,Fecha,Vence, PersonaId,Concepto,Monto,Balance


using System.ComponentModel.DataAnnotations;

public class Prestamo
{
    [Key]

    public int PrestamoId { get; set; }
    public DateTime Fecha { get; set; }=DateTime.Today;
    public DateTime Vence { get; set; }
    public int PersonaId { get; set; }
    public String? Concepto { get; set; }
    public double Balance { get; set; }
    public double Monto { get; set; }
}
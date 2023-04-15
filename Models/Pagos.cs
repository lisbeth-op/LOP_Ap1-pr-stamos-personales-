//Campos: Pagos(PagoId, Fecha, PersonaId, Concepto, Monto) PagosDetalle(Id, PagoId, PrestamoId, ValorPagado) 

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Pagos
{
    [Key]
    public int PagoId { get; set; }
    public DateTime Fecha { get; set; }=DateTime.Today;
    public int PersonaId { get; set; }
     [Required(ErrorMessage = "Concepto requerido")]
    public string Concepto { get; set; }=string.Empty;
    [Required(ErrorMessage = "Campo  requerido")]

     [Range(minimum:100,maximum:double.MaxValue,ErrorMessage ="Cantidad invalida")]
    public double Monto { get; set; }

    [ForeignKey("PagoId")]
    public virtual List<PagosDetalle> PagoDetalles { get; set; } = new List<PagosDetalle>();
}
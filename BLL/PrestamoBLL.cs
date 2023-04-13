using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Models;


public class PrestamoBLL
{
    private Contexto contexto;

    public PrestamoBLL(Contexto _contexto)
    {
        contexto = _contexto;
    }
  
    public List<Prestamo> GetList(Expression<Func<Prestamo, bool>> condicion)
    {

        return contexto.Prestamo.AsNoTracking().Where(condicion).ToList();
    }

   public bool Eliminar(Prestamo prestamo)
{
    var persona = contexto.Personas.Find(prestamo.PersonaId);

    if (persona != null)
    {
        persona.Balance -= prestamo.Balance;
        contexto.Entry(persona).State = EntityState.Modified;
    }

    contexto.Entry(prestamo).State = EntityState.Deleted;

    return contexto.SaveChanges() > 0;
}


    public bool Existe(int PrestamoId)
{
    var prestamo = contexto.Prestamo.Find(PrestamoId);
    return prestamo != null;
}

private bool Insertar(Prestamo prestamo)
{
    var persona = contexto.Personas.Find(prestamo.PersonaId);

    contexto.Prestamo.Add(prestamo);

    persona.Balance += prestamo.Balance;

    return contexto.SaveChanges() > 0;
}


private bool Modificar(Prestamo prestamo)
{
    var pagosDetalle = contexto.Set<PagosDetalle>()
        .Where(d => d.PrestamoId == prestamo.PrestamoId)
        .ToList();

    var persona = contexto.Personas.Find(prestamo.PersonaId);
    var prestamoAnterior = contexto.Prestamo.Find(prestamo.PrestamoId);

    if (persona != null && prestamoAnterior != null)
    {
        persona.Balance -= prestamoAnterior.Balance;
    }

    prestamo.Balance = prestamo.Monto - pagosDetalle.Sum(d => d.ValorPagado);

    if (persona != null)
    {
        persona.Balance += prestamo.Balance;
    }

    contexto.Entry(prestamo).State = EntityState.Modified;

    return contexto.SaveChanges() > 0;
}

public bool Guardar(Prestamo prestamo)
{
    return Existe(prestamo.PrestamoId) ? Modificar(prestamo) : Insertar(prestamo);
}

   
 
    public Prestamo? Buscar(int PrestamoId)
    {
        return contexto.Prestamo
         .AsNoTracking()
         .SingleOrDefault(p => p.PrestamoId == PrestamoId);

    }




}
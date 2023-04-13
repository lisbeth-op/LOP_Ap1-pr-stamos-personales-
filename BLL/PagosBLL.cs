using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Models;



public class PagosBLL
{

    private Contexto contexto;

    public PagosBLL(Contexto _contexto)
    {
        contexto = _contexto;
    }

    private bool Existe(int PagoId)
    {
        return contexto.Pagos.Any(p => p.PagoId == PagoId);
    }

   

    public List<Pagos> GetList(Expression<Func<Pagos, bool>> condicion)

    {
        return contexto.Pagos.AsNoTracking().Where(condicion).ToList();

    }

    public Pagos? Buscar(int pagoId)
    {
        return contexto.Pagos
         .AsNoTracking()
         .SingleOrDefault(p => p.PagoId == pagoId);

    }

public bool Eliminar(Pagos pago)
{
    var persona = contexto.Personas.Find(pago.PersonaId);

    if (pago.PagoDetalles != null)
    {
        foreach (var detalle in pago.PagoDetalles)
        {
            var prestamo = contexto.Prestamo.Find(detalle.PrestamoId);
            if (prestamo != null)
            {
                prestamo.Balance += detalle.ValorPagado;
                contexto.Entry(prestamo).State = EntityState.Modified;
            }

            if (persona != null)
            {
                persona.Balance += detalle.ValorPagado;
                contexto.Entry(persona).State = EntityState.Modified;
            }
        }
    }

    var pagosDetalleAEliminar = contexto.Set<PagosDetalle>().Where(pd => pd.PagoId == pago.PagoId);
    contexto.Set<PagosDetalle>().RemoveRange(pagosDetalleAEliminar);

    contexto.Entry(pago).State = EntityState.Deleted;

    return contexto.SaveChanges() > 0;
}
    
    private bool Insertar(Pagos pagos)
    {
        var persona = contexto.Personas.Find(pagos.PersonaId);
        if(pagos.PagoDetalles != null){
             foreach (var detalle in pagos.PagoDetalles)
            {
                var prestamo = contexto.Prestamo.Find(detalle.PrestamoId);
                
                if(prestamo!=null){
                     prestamo.Balance -= detalle.ValorPagado;
                    contexto.Entry(prestamo).State = EntityState.Modified;
                }
                
                if(persona!=null){
                    persona.Balance-=detalle.ValorPagado;
                    contexto.Entry(persona).State = EntityState.Modified;  
                 }
              
            }  
    }
    contexto.Pagos.Add(pagos);
        return contexto.SaveChanges() > 0;
    }


private bool Modificar(Pagos pago)
{
    var pagoAnterior = contexto.Pagos
        .Where(p => p.PagoId == pago.PagoId)
        .Include(p => p.PagoDetalles)
        .AsNoTracking()
        .SingleOrDefault();

    if (pagoAnterior == null || pagoAnterior.PagoDetalles == null)
        return false;

    foreach (var detalle in pagoAnterior.PagoDetalles)
    {
        var prestamo = contexto.Prestamo.Find(detalle.PrestamoId);
        var persona = contexto.Personas.Find(prestamo?.PersonaId ?? pago.PersonaId);
        
        if (prestamo == null || persona == null)
            continue;
        
        prestamo.Balance += detalle.ValorPagado;
        contexto.Entry(prestamo).State = EntityState.Modified;
        persona.Balance += detalle.ValorPagado;
        contexto.Entry(persona).State = EntityState.Modified;
    }
    
    if (pago.PagoDetalles != null)
    {
        foreach (var detalle in pago.PagoDetalles)
        {
            var prestamo = contexto.Prestamo.Find(detalle.PrestamoId);
            var persona = contexto.Personas.Find(prestamo?.PersonaId ?? pago.PersonaId);
            
            if (prestamo == null || persona == null)
                continue;
            
            prestamo.Balance -= detalle.ValorPagado;
            contexto.Entry(prestamo).State = EntityState.Modified;
            persona.Balance -= detalle.ValorPagado;
            contexto.Entry(persona).State = EntityState.Modified;
        }
    }

    var pagosDetalleAEliminar = contexto.Set<PagosDetalle>().Where(pd => pd.PagoId == pago.PagoId);
    contexto.Set<PagosDetalle>().RemoveRange(pagosDetalleAEliminar);
    contexto.Entry(pago).State = EntityState.Modified;

    return contexto.SaveChanges() > 0;
}


    public bool Guardar(Pagos pagos)
    {
        
        if (!Existe(pagos.PagoId))
            return this.Insertar(pagos);
        else
            return this.Modificar(pagos);

    }

}


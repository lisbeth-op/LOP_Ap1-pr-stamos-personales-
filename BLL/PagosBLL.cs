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
        return contexto.Pagos.Where(condicion).ToList();

    }

    public Pagos? Buscar(int pagoId)
    {
        return contexto.Pagos
         .AsNoTracking()
         .SingleOrDefault(p => p.PagoId == pagoId);

    }

    public bool Eliminar(Pagos pagos)
    {
        if (!Existe(pagos.PagoId))
        {
            return false;
        }
        contexto.Entry(pagos).State = EntityState.Deleted;
        return contexto.SaveChanges() > 0;
    }

    private bool Insertar(Pagos pagos)
    {
        contexto.Pagos.Add(pagos);
        return contexto.SaveChanges() > 0;
    }


    private bool Modificar(Pagos pagos)
    {
        contexto.Database.ExecuteSqlRaw($"Delete From PagosDetalle where PagoId={pagos.PagoId}");
        foreach (var pagoAnterior in pagos.PagoDetalles)
        {
            contexto.Entry(pagoAnterior).State=EntityState.Added;
        }
        contexto.Entry(pagos).State = EntityState.Modified;
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


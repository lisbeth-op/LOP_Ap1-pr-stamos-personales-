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
    public bool Existe(int PrestamoId)
    {
        return contexto.Prestamo.Any(p => p.PrestamoId == PrestamoId);

    }
    public List<Prestamo> GetList (Expression<Func<Prestamo, bool >> condicion){

        return contexto.Prestamo.Where(condicion).ToList();
    }

    public bool Eliminar (Prestamo prestamo){
           contexto.Entry(prestamo).State=EntityState.Deleted;
            return contexto.SaveChanges()>0;

    }

      private bool Insertar(Prestamo prestamo)
    {
        contexto.Prestamo.Add(prestamo);
        return contexto.SaveChanges() > 0;
    }


    private bool Modificar(Prestamo prestamo)
    {
        contexto.Entry(prestamo).State = EntityState.Modified;
    return contexto.SaveChanges() > 0;
    }

    public bool Guardar(Prestamo prestamo)
    {
        if (!Existe(prestamo.PrestamoId))
            return this.Insertar(prestamo);
        else
            return this.Modificar(prestamo);

    }
     public Prestamo? Buscar(int PrestamoId)
    {
        return contexto.Prestamo
         .AsNoTracking()
         .SingleOrDefault(p => p.PrestamoId == PrestamoId);

    }




}
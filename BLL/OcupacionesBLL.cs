using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Models;

public class OcupacionesBLL{
private Contexto _contexto;

public OcupacionesBLL(Contexto contexto){

_contexto= contexto;

}

public bool Existe(int OcupacionId){


    return _contexto.Ocupaciones.Any(o => o.OcupacionId== OcupacionId);

}

public bool ExisteDescripcion(string Descripcion){
    return _contexto.Ocupaciones.Any(o => o.Descripcion.ToLower().Equals(Descripcion.ToLower()));
}
private bool Insertar (Ocupaciones ocupacion){
_contexto.Ocupaciones.Add(ocupacion);
return _contexto.SaveChanges() >0;

}

private bool Modificar (Ocupaciones ocupacion){
    _contexto.Entry(ocupacion).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
    return _contexto.SaveChanges()>0;
}

public bool Guardar (Ocupaciones ocupacion){

 if (!Existe(ocupacion.OcupacionId))
 return this.Insertar(ocupacion);
 else 
    return this.Modificar(ocupacion);
}

public bool Eliminar (Ocupaciones ocupacion){
    _contexto.Entry(ocupacion).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
    return _contexto.SaveChanges() > 0;
}

public Ocupaciones? Buscar (int OcupacionId){
    return _contexto.Ocupaciones
    .AsNoTracking()
    .SingleOrDefault (o => o.OcupacionId == OcupacionId);
}

public List<Ocupaciones> GetList (Expression<Func<Ocupaciones, bool>> criterio){
    return _contexto.Ocupaciones
    .AsNoTracking()
    .Where(criterio)
    .ToList();
}








}
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Models;



public class PersonasBLL
{

    private Contexto contexto;

    public PersonasBLL(Contexto _contexto)
    {
        contexto = _contexto;
    }

    private bool Existe(int PersonaId)
    {
        return contexto.Personas.Any(p => p.PersonaId == PersonaId);
    }

    private bool ExisteDescripcion(string Nombre)
    {
        return contexto.Personas.Any(p => p.Nombre.ToUpper().Equals(Nombre.ToUpper()));

    }

    public List<Personas> GetPersonas(Expression<Func<Personas, bool>> condicion)

    {
        return contexto.Personas.Where(condicion).ToList();

    }

    public Personas? Buscar(int PersonaID)
    {
        return contexto.Personas
         .AsNoTracking()
         .SingleOrDefault(p => p.PersonaId == PersonaID);

    }

    public bool Eliminar(Personas persona)
    {
        contexto.Entry(persona).State = EntityState.Deleted;
        return contexto.SaveChanges() > 0;
    }

    private bool Insertar(Personas persona)
    {
        contexto.Personas.Add(persona);
        return contexto.SaveChanges() > 0;
    }


    private bool Modificar(Personas persona)
    {
        contexto.Entry(persona).State = EntityState.Modified;
    return contexto.SaveChanges() > 0;
    }

    public bool Guardar(Personas persona)
    {
        if (!Existe(persona.PersonaId))
            return this.Insertar(persona);
        else
            return this.Modificar(persona);

    }

}


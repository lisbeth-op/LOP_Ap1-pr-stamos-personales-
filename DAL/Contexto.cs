 using Microsoft.EntityFrameworkCore;
 using Models;



public class Contexto:DbContext{
    public DbSet < Ocupaciones> Ocupaciones{ get; set;}
    public DbSet<Personas> Personas{get; set;}
     public DbSet<Prestamo> Prestamo {get; set;}
     public DbSet<Pagos> Pagos {get; set;}
    public Contexto(DbContextOptions<Contexto> options): base(options){

    }


}

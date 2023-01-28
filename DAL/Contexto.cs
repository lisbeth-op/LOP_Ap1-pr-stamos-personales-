 using Microsoft.EntityFrameworkCore;
 using Models;



public class Contexto:DbContext{
    public DbSet < Ocupaciones> Ocupaciones{ get; set;}
    
    public Contexto(DbContextOptions<Contexto> options): base(options){

    }


}
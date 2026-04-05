using mvc_backend_1.Models;
using Microsoft.EntityFrameworkCore;
namespace mvc_backend_1.Context;

public class ReserveContext : DbContext
{
    public ReserveContext(DbContextOptions<ReserveContext> options) : base(options){}
    public DbSet<Reserve> reserve {get; set;}
}
using Microsoft.EntityFrameworkCore;
using Corgi.Data.Entities;

namespace Corgi.Data;

public class CorgiDbContext(DbContextOptions<CorgiDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<SongShareEntity> SongShares => Set<SongShareEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasKey(u => u.Id);
        modelBuilder.Entity<SongShareEntity>().HasKey(s => s.Id);
    }
}

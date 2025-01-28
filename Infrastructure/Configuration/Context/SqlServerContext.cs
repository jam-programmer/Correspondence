

using Infrastructure.Configuration.Extension;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Configuration.Context;

public class SqlServerContext : DbContext

{
    public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.HasSequence<long>("DocumentNumberGenerator")
        //     .StartsAt(1000).IncrementsBy(2).HasMax(long.MaxValue);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.AppendDbSetOfEntity();
        base.OnModelCreating(builder);
    }
}

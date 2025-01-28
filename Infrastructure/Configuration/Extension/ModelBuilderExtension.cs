

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Configuration.Extension;


internal static class ModelBuilderExtension
{
    internal static void AppendDbSetOfEntity(this ModelBuilder builder)
    {
        Assembly? assembly = Assembly.GetAssembly(typeof(BaseEntity));
        if (assembly is not null)
        {
            List<Type> entities = assembly.GetTypes()
               .Where(w => w.IsClass
               && w.IsPublic
               && w.BaseType != null && w.BaseType == typeof(BaseEntity))
               .ToList();
            entities.ForEach(entity => builder.Entity(entity));
        }
    }
}
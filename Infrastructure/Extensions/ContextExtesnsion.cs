using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class ContextExtesnsion
{
   public static DbSet<T> DbSet<T>(this DbContext context) where  T : class
   {
      return context.Set<T>();
   }
}
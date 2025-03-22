using Microsoft.EntityFrameworkCore;

namespace DepartureBoard.Api.Postgres;

public class PostgresTruncator(DbContext context)
{
    public void Truncate()
    {
        var tableNames = context.Model.GetEntityTypes()
            .Select(t => t.GetTableName())
            .Distinct()
            .ToList();
        
        foreach (var tableName in tableNames)
        {
            var query = $"TRUNCATE TABLE public.\"{tableName}\" RESTART IDENTITY CASCADE";
            context.Database.ExecuteSqlRaw(query);
        }
    }
}
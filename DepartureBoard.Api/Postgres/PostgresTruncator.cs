using Microsoft.EntityFrameworkCore;

namespace DepartureBoard.Api.Postgres;

public class PostgresTruncator(DbContext context)
{
    private readonly DbContext _context = context;
    
    public void Truncate()
    {
        var tableNames = _context.Model.GetEntityTypes()
            .Select(t => t.GetTableName())
            .Distinct()
            .ToList();
        
        foreach (var tableName in tableNames)
        {
            var query = $"TRUNCATE TABLE public.\"{tableName}\" RESTART IDENTITY CASCADE";
            _context.Database.ExecuteSqlRaw(query);
        }
    }
}
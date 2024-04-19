using Microsoft.Data.SqlClient;
using RestAPI.Repositories.SqlClient;

namespace RestAPI.Repositories;

internal abstract class BaseRepository(IConfiguration configuration)
{
    protected abstract string TableName { get; }
    private string ConnectionString => configuration.GetConnectionString(nameof(ConnectionStrings.DefaultConnection))!;
    
    protected async Task ExecuteSqlAsync(string query, Action<SqlParameterCollection> onParametrizationAction, Action<SqlReadRowModel> onReadAction, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync(cancellationToken);
            
        await using var command = new SqlCommand(query, connection);
        onParametrizationAction(command.Parameters);
            
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var ordinalsDict = RetrieveOrdinals(reader);
        
        while (await reader.ReadAsync(cancellationToken))
        {
            onReadAction(new(reader, ordinalsDict));
        }
    }

    private Dictionary<string, int> RetrieveOrdinals(SqlDataReader reader)
    {
        var ordinalsDict = new Dictionary<string, int>();
        for (var i = 0; i < reader.FieldCount; i++)
        {
            ordinalsDict.Add(reader.GetName(i).ToLower(), i);
        }

        return ordinalsDict;
    }
}
using Microsoft.Data.SqlClient;

namespace RestAPI.Repositories.SqlClient;

internal record SqlReadRowModel(SqlDataReader Reader, Dictionary<string, int> Columns);
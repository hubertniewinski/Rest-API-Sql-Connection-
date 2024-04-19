using Microsoft.Data.SqlClient;
using RestAPI.Exceptions;
using RestAPI.Models;
using RestAPI.Repositories.SqlClient;

namespace RestAPI.Repositories;

internal partial class AnimalsRepository(IConfiguration configuration)
    : BaseRepository(configuration), IAnimalsRepository
{
    protected override string TableName => nameof(Animal);

    public async Task CreateAsync(Animal model, CancellationToken cancellationToken)
    {
        var query = $"INSERT INTO {TableName} {AnimalSqlColumns.UpdateColumns.AsInsertQuery()}";

        await ExecuteSqlAsync(query, @params => @params.AddWithValue(AnimalSqlColumns.UpdateColumns, model),
            _ => { }, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var query = $"DELETE FROM {TableName} WHERE {AnimalSqlColumns.Id.AsEqualsQuery()}";
        var deleted = false;

        await ExecuteSqlAsync(query, @params => @params.AddWithValue(AnimalSqlColumns.Id.Parameter, id), _ => deleted = true,
            cancellationToken);

        if (!deleted)
        {
            throw new AnimalNotFoundException(id);
        }
    }

    public async Task<IEnumerable<Animal>> GetByNameAsync(string name, CancellationToken cancellationToken) => 
        await GetAllInternalAsync($"WHERE {AnimalSqlColumns.Name.AsEqualsQuery()}",
            @params => @params.AddWithValue(AnimalSqlColumns.Name.Parameter, name), cancellationToken);
    
    public async Task<IEnumerable<Animal>> GetAllAsync(string orderBy, CancellationToken cancellationToken = default)
    {
        var orderByColumn = AnimalSqlColumns.SortableColumns.FirstOrDefault(x => x.Name.Equals(orderBy, StringComparison.CurrentCultureIgnoreCase));

        if (orderByColumn is null)
        {
            throw new ValidationException($"Unsupported {nameof(orderBy)} parameter");
        }

        return await GetAllInternalAsync($"ORDER BY [{orderByColumn.Name}] ASC",
            (_) => {}, cancellationToken);
    }
    
    public async Task<IEnumerable<Animal>> GetAllAsync(CancellationToken cancellationToken) 
        => await GetAllInternalAsync(cancellationToken: cancellationToken);

    private async Task<IEnumerable<Animal>> GetAllInternalAsync(string additionalSql = "",
        Action<SqlParameterCollection>? onParametrizationAdditionalAction = null, CancellationToken cancellationToken = default)
    {
        var query = $"SELECT * FROM {TableName} {additionalSql}";
        var animals = new List<Animal>();

        await ExecuteSqlAsync(query, x => onParametrizationAdditionalAction?.Invoke(x),
            x => animals.Add(AnimalSqlColumns.RetrieveDomainModel(x)), cancellationToken);

        return animals;
    }

    public async Task<Animal> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var query = $"SELECT * FROM {TableName} WHERE {AnimalSqlColumns.Id.AsEqualsQuery()}";

        Animal? model = null;
        await ExecuteSqlAsync(query, @params => @params.AddWithValue(AnimalSqlColumns.Id.Parameter, id),
            x => model = AnimalSqlColumns.RetrieveDomainModel(x), cancellationToken);
        return model ?? throw new AnimalNotFoundException(id);
    }

    public async Task UpdateAsync(Animal model, CancellationToken cancellationToken)
    {
        var query = $"UPDATE {TableName} SET {AnimalSqlColumns.UpdateColumns.AsEqualsQuery()} " +
                    $"WHERE {AnimalSqlColumns.Id.AsEqualsQuery()}";

        await ExecuteSqlAsync(query, @params => @params.AddWithValue(AnimalSqlColumns.All, model), reader => { },
            cancellationToken);
    }
}
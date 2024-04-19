using RestAPI.Models;
using RestAPI.Repositories.SqlClient;

namespace RestAPI.Repositories;

internal partial class AnimalsRepository
{
    private static class AnimalSqlColumns
    {
        public static readonly SqlColumn<Animal> Id = Create(nameof(Animal.IdAnimal), a => a.IdAnimal);
        public static readonly SqlColumn<Animal> Name = Create(nameof(Animal.Name), a => a.Name);
        public static readonly SqlColumn<Animal> Description = Create(nameof(Animal.Description), a => a.Description);
        public static readonly SqlColumn<Animal> Category = Create(nameof(Animal.Category), a => a.Category);
        public static readonly SqlColumn<Animal> Area = Create(nameof(Animal.Area), a => a.Area);

        public static readonly SqlColumn<Animal>[] UpdateColumns = [Name, Description, Category, Area];
        public static SqlColumn<Animal>[] SortableColumns => UpdateColumns;
        public static readonly SqlColumn<Animal>[] All = [Id, Name, Description, Category, Area];

        private static SqlColumn<Animal> Create(string name, Func<Animal, object?> getDomainModelValue)
            => new(name, name.AsParameter(), getDomainModelValue);

        public static Animal RetrieveDomainModel(SqlReadRowModel r)
        {
            var id = r.Reader.GetInt32(r.Columns[Id.Name]);
            var name = r.Reader.GetString(r.Columns[Name.Name]);
            var description = r.Reader.GetValue(r.Columns[Description.Name])?.ToString();
            var category = r.Reader.GetString(r.Columns[Category.Name]);
            var area = r.Reader.GetString(r.Columns[Area.Name]);

            return Animal.Create(id, name, description, category, area);
        }
    }
}
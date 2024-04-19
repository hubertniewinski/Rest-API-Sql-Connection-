﻿using Microsoft.Data.SqlClient;

namespace RestAPI.Repositories.SqlClient;

internal static class SqlExtensions
{
	public static string AsParameter(this string value) => $"@{value}";
	public static string AsEqualsQuery<T>(this SqlColumn<T> sqlColumn) => $"{sqlColumn.Name} = {sqlColumn.Parameter}";
	public static string AsEqualsQuery<T>(this SqlColumn<T>[] sqlColumns) =>
		string.Join(", ", sqlColumns.Select(x => x.AsEqualsQuery()));
	public static string AsInsertQuery<T>(this SqlColumn<T>[] sqlColumns) =>
		$"({string.Join(", ", sqlColumns.Select(x => x.Name))}) VALUES ({string.Join(", ", sqlColumns.Select(x => x.Parameter))})";
	public static void AddWithValue<T>(this SqlParameterCollection @params, IEnumerable<SqlColumn<T>> columns, T model)
	{
		foreach (var column in columns)
		{
			@params.AddWithValue(column.Parameter, column.GetDomainModelValue(model));
		}
	}
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Lab6.Core
{
	public class PgQueryable : IDbQueryable
	{
		private readonly string _connectionString;

		public PgQueryable(string connectionString)
		{
			_connectionString = connectionString;
		}

		public void SendSqlQuery(string sql, KeyValuePair<string, object>[] arguments = null, CommandType type = CommandType.Text)
		{
			SendSqlQueryAsync(sql, arguments, type).Wait(TimeSpan.MaxValue);
		}

		public async Task SendSqlQueryAsync(string sql, KeyValuePair<string, object>[] arguments = null, CommandType type = CommandType.Text)
		{
			arguments = arguments ?? new KeyValuePair<string, object>[0];
			using (var conn = new NpgsqlConnection(_connectionString))
			{
				await conn.OpenAsync();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = sql;
					cmd.Connection = conn;
					cmd.CommandType = type;

					foreach (var arg in arguments)
					{
						if (arg.Value is string)
						{
							cmd.Parameters.AddWithValue(arg.Key, NpgsqlTypes.NpgsqlDbType.Text, arg.Value);
						}
						else if (arg.Value is int)
						{
							cmd.Parameters.AddWithValue(arg.Key, NpgsqlTypes.NpgsqlDbType.Integer, arg.Value);
						}
						else
						{
							cmd.Parameters.AddWithValue(arg.Key, arg.Value);
						}
					}

					cmd.Prepare();

					await cmd.ExecuteReaderAsync();
				}
			}
		}
	}
}
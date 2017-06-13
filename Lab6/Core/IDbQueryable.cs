using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Lab6.Core
{
	public interface IDbQueryable
	{
		void SendSqlQuery(string sql, KeyValuePair<string, object>[] arguments = null, CommandType type = CommandType.Text);
		Task SendSqlQueryAsync(string sql, KeyValuePair<string, object>[] arguments = null, CommandType type = CommandType.Text);
	}
}
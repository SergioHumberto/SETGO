using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.DAL.Properties;

namespace WebApplicationTemplate.DAL
{
	public static class DAL
	{
        /* private method */

		private static ISqlMapper Map
		{
			get
			{
				return Mapper.Instance();
			}
		}

        /* public methods */

        public static string ConnectionString
        {
            get { return Map.DataSource.ConnectionString; }
        }

        public static void BeginTransaction()
		{
			Map.BeginTransaction();
		}

		public static void CommitTransaction()
		{
			Map.CommitTransaction();
		}

		public static void RollbackTransaction()
		{
			Map.RollBackTransaction();
		}

        /* internal methods */

		internal static IList<T> QueryForList<T>(string statementName, object parameterObject)
		{
			return Map.QueryForList<T>(statementName, parameterObject);
		}

		internal static T QueryForObject<T>(string statementName, object parameterObject)
		{
			IList<T> list = Map.QueryForList<T>(statementName, parameterObject);

			if (list.Count == 1)
			{
				return list[0];
			}
			else if (list.Count > 1)
			{
				throw new DataAccessException(string.Format(Resources.NotExpectedMultipleRecordsInStatement, statementName));
			}

			return default(T);
		}

		internal static void Insert(string statementName, object parameterObject)
		{
			Map.Insert(statementName, parameterObject);
		}

		internal static int Update(string statementName, object parameterObject)
		{
			return Map.Update(statementName, parameterObject);
		}

		internal static int Delete(string statementName, object parameterObject)
		{
			return Map.Delete(statementName, parameterObject);
		}

		internal static ParametrizedStatement Statement(string statementName)
		{
			return new ParametrizedStatement(statementName);
		}

		internal class ParametrizedStatement
		{
			private readonly string statementName;
			private readonly Dictionary<string, object> parameters;

			internal ParametrizedStatement(string statementName)
			{
				this.statementName = statementName;
				this.parameters = new Dictionary<string, object>();
			}

			internal ParametrizedStatement AddParameter(string name, object value)
			{
				parameters.Add(name, value);
				return this;
			}

			internal IList<T> QueryForList<T>()
			{
				return DAL.QueryForList<T>(statementName, parameters);
			}

			internal T QueryForObject<T>()
			{
				return DAL.QueryForObject<T>(statementName, parameters);
			}

            internal int Update()
            {
                return DAL.Update(statementName, parameters);
            }

            internal void Insert()
            {
                DAL.Insert(statementName, parameters);
            }

            internal int Delete()
            {
                return DAL.Delete(statementName, parameters);
            }
        }
	}
}

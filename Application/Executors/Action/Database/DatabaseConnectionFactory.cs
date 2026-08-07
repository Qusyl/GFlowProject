
using System.Data.Common;
using MySqlConnector;
using Npgsql;


namespace Application.Executors.Action.Database
{
    public class DatabaseConnectionFactory
    {
        public static DbConnection Create(SqlConnectionConfiguration configuration)
        {
            var connection = BuildConnection(configuration);
            return configuration.DatabaseType switch
            {
                DatabaseType.Mysql => new MySqlConnection(connection),
                DatabaseType.Postgresql => new NpgsqlConnection(connection),
                _ => throw new NotSupportedException($"DBMS {configuration.DatabaseType} not supported")
            };
        }

        private static string BuildConnection(SqlConnectionConfiguration configuration)
        {
            var baseString = $"Host={configuration.ConnectionParameters.Host};Database={configuration.ConnectionParameters.Database};Username={configuration.ConnectionParameters.Username};Password={configuration.ConnectionParameters.Password}";

            return configuration.DatabaseType switch
            {
                DatabaseType.Postgresql  => BuildPsqlConnection(configuration.ConnectionParameters, baseString),
                DatabaseType.Mysql => BuildMysqlConnection(configuration.ConnectionParameters),
                _ => throw new NotSupportedException($"DBMS {nameof(configuration.DatabaseType)} not supported!")
            }; 
        }

        private static string BuildMysqlConnection(ConnectionParameters parameters)
        {
            var parts = new List<string>
            {
                $"Server={parameters.Host}",
                $"Port={parameters.Port}",
                $"Database={parameters.Database}",
                $"Uid={parameters.Username}",
                $"Pwd={parameters.Password}"
            };

            if (parameters.Pooling.HasValue)
            {
                parts.Add($"Pooling={parameters.Pooling}");
            }
            if (!string.IsNullOrEmpty(parameters.SslMode.ToString()))
            {
                parts.Add($"SslMode={parameters.SslMode}");
            }
            if (parameters.MinPoolSize.HasValue)
            {
                parts.Add($"Minimum Pool Size={parameters.MinPoolSize}");
            }
            if (parameters.MaxPoolSize.HasValue)
            {
                parts.Add($"Maximum Pool Size={parameters.MaxPoolSize}");
            }
            if (parameters.TimeOut.HasValue)
            {
                parts.Add($"Connection Timeout={parameters.TimeOut}");
            }
            if (parameters.AllowUserVariables.HasValue)
            {
                parts.Add($"AllowUserVariables={parameters.AllowUserVariables}");
            }
            if (!string.IsNullOrEmpty(parameters.CharacterSet))
            {
                parts.Add($"CharacterSet={parameters.CharacterSet}");
            }

            return string.Join(";", parts);
        }

        private  static string BuildPsqlConnection(ConnectionParameters parameters, string baseString)
        {
            var parts = new List<string> { baseString };
            if (parameters.Port.HasValue)
            {
                parts.Add($"Port={parameters.Port}");
            }
            if (parameters.Pooling.HasValue)
            {
                parts.Add($"Pooling={parameters.Pooling}");
            }
            
            // if (parameters.MinPoolSize.HasValue)
            // {
            //     parts.Add($"Minimum Pool Size={parameters.MinPoolSize}");
            // }
            // if (parameters.MaxPoolSize.HasValue)
            // {
            //     parts.Add($"Maximum Pool Size={parameters.MaxPoolSize}");
            // }
            if (parameters.TimeOut.HasValue)
            {
                parts.Add($"CommandTimeout={parameters.TimeOut}");
            }
            if (!string.IsNullOrEmpty(parameters.SearchPath))
            {
                parts.Add($"SearchPath={parameters.SearchPath}");
            }

            return string.Join(";", parts);
        }
    }
}
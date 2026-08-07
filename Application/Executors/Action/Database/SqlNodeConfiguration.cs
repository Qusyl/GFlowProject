using System.Text.Json.Serialization;


namespace Application.Executors.Action.Database
{
    public sealed class SqlNodeConfiguration {

        
        public QueryDefinition Definition { get; init; }
  
        public SqlConnectionConfiguration SqlConnection { get; init; }

        public SqlNodeConfiguration(QueryDefinition definition, SqlConnectionConfiguration sqlConnection)
        {
            Definition = definition;
            SqlConnection = sqlConnection;
           
        }
    }
}
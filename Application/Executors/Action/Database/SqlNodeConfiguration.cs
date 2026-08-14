using System.Text.Json.Serialization;
using Application.Executors.Action.Database.Definitions;


namespace Application.Executors.Action.Database
{
    public sealed class SqlNodeConfiguration {

        
        public RawDefinition Definition { get; init; }
  
        public SqlConnectionConfiguration SqlConnection { get; init; }

        public SqlNodeConfiguration(RawDefinition definition, SqlConnectionConfiguration sqlConnection)
        {
            Definition = definition;
            SqlConnection = sqlConnection;
           
        }
    }
}
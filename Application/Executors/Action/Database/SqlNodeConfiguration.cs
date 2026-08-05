using System.Text.Json.Serialization;


namespace Application.Executors.Action.Database
{
    public sealed class SqlNodeConfiguration {

        [JsonPropertyName("query")]
        public QueryDefinition Definition { get; init; }
        [JsonPropertyName("connection")]
        public SqlConnectionConfiguration SqlConnection { get; init; }

        public SqlNodeConfiguration(QueryDefinition definition, SqlConnectionConfiguration sqlConnectionConfiguration)
        {
            Definition = definition;
            SqlConnection = sqlConnectionConfiguration;
           
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Context;
using Application.Exceptions;
using Application.Executors.Configurations;

namespace Application.Executors.Action.Database.Postgresql
{
    public class ActionPsqlExecutor : INodeExecutors
    {

        public async Task<NodeResult> ExecuteAsync(NodeExecutionContext context)
        {
            var configuration = context.Node.Node.GetProperty<DatabaseConfiguration>("DatabaseConfiguration");

            var queryType = context.Node.Node.GetProperty<QueryDefinition>("Query");

            if (configuration is null)
            {
                return NodeResult.Failure(new NullNodePropertyReferenceException("not found property"));
            }

            try
            {
                using var connection = DatabaseConnectionFactory.Create(configuration);

                await connection.OpenAsync();

                
            }
            

        }
    }
}
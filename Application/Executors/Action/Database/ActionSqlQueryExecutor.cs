using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Context;
using Application.Exceptions;
using Application.Executors.Action.Database.Definitions;
using Application.Executors.Action.Database.Dialects;
using Application.Executors.Action.Database.Handlers;
using Application.Executors.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Executors.Action.Database
{
    [NodeExecutor("SqlAction")]
    public class ActionSqlQueryExecutor : INodeExecutors
    {
        private readonly ISqlDialectFactory _dialectFactorty;

        private readonly IServiceProvider _serviceProvider;
        public ActionSqlQueryExecutor(ISqlDialectFactory dialectFactory, IServiceProvider provider)
        {
            _dialectFactorty = dialectFactory;
            _serviceProvider = provider;
        }
        public async Task<NodeResult> ExecuteAsync(NodeExecutionContext context)
        {
            var configuration = context.Node.Node.GetProperty<SqlNodeConfiguration>("SqlConfiguration");

          
            if (configuration is null)
            {
                return NodeResult.Failure(new NullNodePropertyReferenceException($"Not found property with name {nameof(SqlNodeConfiguration)}"));
            }

            var definition = configuration.Definition;
             using var sqlConnection = DatabaseConnectionFactory.Create(configuration.SqlConnection);
            try
            {
            
                await sqlConnection.OpenAsync();

                var dialect = _dialectFactorty.GetDialect(configuration.SqlConnection.DatabaseType);
                var handler = _serviceProvider.GetRequiredService<ISqlQueryHandler>() as RawSqlQueryHandler;
                if(handler is null)
                {
                    throw new NullReferenceException("Handler not found");
                }
                var queryResult = await handler.HandleAsync(definition, sqlConnection, dialect);
               

                context.WorkflowContext.SetVariable("QueryResult", new VariableValue(new VariableDefenition("QueryResult", Logic.Logics.ArgumentType.Integer), queryResult));

                return NodeResult.Success(context.Node.Descriptor.GetOutcomingPorts());

            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return NodeResult.Failure(ex);
            }
        }
    }
}
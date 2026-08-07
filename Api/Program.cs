
using Api.Extensions;
using Application.Executors.Action.Database.Dialects;
using Application.Executors.Action.Http.Request;
using Application.Executors.Configurations;
using Application.Runtime.Workflow;
using MainApi.ExeptionHandler;
using MainApi.Extensions;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IExecutorResolver, ExecutorRegistry>();
builder.Services.AddSingleton<IWorkflowFactory, WorkflowFactory>();
builder.Services.AddSingleton<ISqlDialectFactory, SqlDialectFactory>();
builder.Services.AddHttpClient<ActionHttpRequestExecutor>().ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
{
    PooledConnectionLifetime = TimeSpan.FromMinutes(15),
    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2)

});
builder.Services.RegisterAllExecutors();
builder.Services.RegisterAllSqlHandlers();
builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "_V_1_";
    config.Title = "WorkFlow API";
    config.Version = "V1";
    config.Description = "Test Workflow API";

});
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<WorkflowExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapControllers();
app.Run();



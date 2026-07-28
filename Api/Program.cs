
using MainApi.ExeptionHandler;


var builder = WebApplication.CreateBuilder(args);

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



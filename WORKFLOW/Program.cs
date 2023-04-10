
global using WORKFLOW.Dao;
global using WORKFLOW.Config;
global using WORKFLOW.Helper;
global using WORKFLOW.Services;
global using WORKFLOW.Model.db;
global using WORKFLOW.Model.dto;
global using WORKFLOW.Model.Shared;
global using WORKFLOW.Model.Context;
global using Newtonsoft.Json;
global using RulesEngine.Models;
global using Microsoft.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics.CodeAnalysis;

//Config Program
var builder = WebApplication.CreateBuilder(args);

//Config Env
string env = builder.Environment.EnvironmentName;
var appconfig = ConfigHelper.loadConfig<AppConfig>(new ConfigurationBuilder(), env);
builder.Services.AddSingleton<AppConfig>(appconfig);

//Config IP
builder.WebHost.UseUrls("http://*:" + appconfig.port);

//Config PostgreSQL Connection
builder.Services.AddDbContext<WorkflowContext>(options =>
{
    options.UseNpgsql(appconfig.pgsql_url);
});

//Config Service
builder.Services.AddScoped<IWorkflowServices, WorkflowServices>();
builder.Services.AddScoped<IWorkflowDao, WorkflowDao>();

//Config Helper Promo
var workflowHelper = new WorkflowHelper();
builder.Services.AddSingleton<IWorkflowHelper>(workflowHelper);

//Config Controller
builder.Services.AddControllers();

//Config Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Get Workflow First
try {
    using (var scope = app.Services.CreateScope()) {
        var workflowService = scope.ServiceProvider.GetRequiredService<IWorkflowServices>();
        workflowHelper.RefreshWorkFlow(await workflowService.SetWorkFlow());
    }

    app.Logger.LogInformation("SUCCESS GET WORKFLOW PROMO");
} catch (Exception ex) {
    app.Logger.LogInformation(ex.Message);
}

//Run Swagger
app.UseSwagger();
app.UseSwaggerUI();

//Run Auth
app.UseAuthorization(); 

//Run Controller
app.MapControllers();

//Run Program
app.Run();

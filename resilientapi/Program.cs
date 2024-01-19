using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.OpenApi.Models;
using resilientapi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IHelloService, HelloService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen( c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1"});
});

var app = builder.Build();

if( app.Environment.IsDevelopment()) 
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI( c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

app.MapGet("/", () => "Hello World!");

app.MapGet("/{name}", ([FromRoute] string name, IHelloService service) => service.Hello(name));

app.MapGet("/date/{date}", (DateTime date) => date.ToString());

app.MapGet("/uniqueidentifier/{id}", (Guid id) => id.ToString());

app.MapMethods("/users/{userId}", new List<string> { "PUT", "PATCH" }, (HttpRequest request) =>
{
    var id = request.RouteValues["id"];
    var lastActivityDate = request.Form["lastactivitydate"];
});

app.MapMethods("/routeName", new List<string> { "OPTIONS", "HEAD", "TRACE" }, () => { 
    // Do action
});


app.MapGet("/provinces/{provinceId:int:max(12)}", (int provinceId) => $"ProvinceId {provinceId}");



app.Run();

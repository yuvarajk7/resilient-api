using System.ComponentModel.DataAnnotations;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.OpenApi.Models;
using resilientapi;
using resilientapi.Models;
using resilientapi.RouteGroups;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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

/* Route Contraints */

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

/* Route groups examples */

app.MapGroup("/countries").GroupCountries();

/* Parameter Bindings */

app.MapPost("/Addresses", ([FromBody] Address address) => {
    return Results.Created();
});

//FromForm - application/x-www-form-urlencoded
app.MapPut("/Addresses/{addressId}", ([FromRoute] int addressId, [FromForm] Address address) => {
    return Results.NoContent();
}).DisableAntiforgery();

app.MapGet("/Addresses", ([FromHeader] string coordinates, [FromQuery] int? limitCountSearch) => {
    return Results.Ok();
});

app.MapGet("/IdList", ([FromQuery] int[] id) =>
{
    return Results.Ok();
});

app.MapGet("/languages", ([FromHeader(Name = "lng")] string[] lng) =>
{
    return Results.Ok(lng);
});

/* Fluent Validators */

app.MapPost("/countries", ([FromBody] Country country, 
    IValidator<Country> validator) => 
{    
    var validationResult = validator.Validate(country);
    if( validationResult.IsValid) 
    {
        return Results.Created();
    }
    return Results.ValidationProblem(validationResult.ToDictionary(),
        statusCode: (int)HttpStatusCode.BadRequest);
});

app.Run();

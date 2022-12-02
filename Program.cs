using Microsoft.OpenApi.Models;
using PizzaStore ;

var builder = WebApplication.CreateBuilder(args);
//adding CORS Service : 
builder.Services.AddCors(option => { });
//adding swagger service  : 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("Version 1 ", new OpenApiInfo
    {
        Title = "Minimal Api ",
        Description = "following the Microsoft learn path ",
        Version = "v1",
    });
});
// Look carefully , swagger service and others are added before the BUILD is done !! 
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API V1");

});

app.MapGet("pizzas/{id}", (int id) => PizzaDb.GetPizza(id));
app.MapGet("/pizzas", () =>PizzaDb.GetPizzas());
app.MapPost("/pizzas", (Pizza pizza) => PizzaDb.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza pizza) => PizzaDb.UpdatePizza(pizza));
app.MapDelete("/pizzas/{id}", (int id) => PizzaDb.RemovePizza(id));
// We run the API to make it ready to listen to Client Requests !! 

app.Run();

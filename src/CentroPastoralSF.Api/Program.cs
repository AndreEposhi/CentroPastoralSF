using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddDataContext();
builder.AddHttpClientConfiguration();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();
builder.AddMediator();
builder.AddEncryptorConfiguration();
builder.AddSubtleCryptoConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoints();

app.Run();


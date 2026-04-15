using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configure .NET 10 Native OpenAPI with Metadata
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "GridERP Management API";
        document.Info.Version = "v1.0.0";
        document.Info.Description = "Enterprise backend for managing inventory, finances, and operations.";
        document.Info.Contact = new OpenApiContact
        {
            Name = "Developer",
            Email = "tleon.dev@gmail.com"
        };
        return Task.CompletedTask;
    });
});

var app = builder.Build();

//  REQUEST PIPELINE ---


if (app.Environment.IsDevelopment())
{
    // Generates the JSON document at /openapi/v1.json
    app.MapOpenApi();


    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "GridERP v1");

        //Sets Swagger as the default launch page
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();


app.UseAuthorization();


app.MapControllers();

app.Run();
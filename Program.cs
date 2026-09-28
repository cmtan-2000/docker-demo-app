using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

var app = builder.Build();

app.MapGet("/db-test", async () =>
{
    await using var connection = new NpgsqlConnection(connectionString);
    await connection.OpenAsync();

    await using var command = new NpgsqlCommand("SELECT version();", connection);

    var result = await command.ExecuteScalarAsync();

    return Results.Ok(result);
});

app.Run();
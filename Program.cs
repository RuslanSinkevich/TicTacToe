using TicTacToe.models;
using TicTacToe.startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

DataGame dataGame = new()
{
    idGame = Guid.NewGuid().ToString(),
    nameGame = "Крестики нолики",
    idPlayer = 1,
    idPlayerNext = 1,
    idPlayerWin = 0,
    data = new sbyte[]{-1,-1,-1,-1,-1,-1,-1,-1,-1}
};


app.MapPost("/game-start", () => dataGame);

//app.MapPost("/game-start", () => MongoDataBase.CreateAsync(dataGame));


app.MapPost("/game-flow", (DataGame game) =>
{
    var data = game.data;
    if (data == null) return dataGame;

    if (data.Length < 8) return dataGame;

    if ((data[0] + data[1] + data[2]) == 3 || (data[0] + data[1] + data[2]) == 27)
        game.idPlayerWin = game.idPlayer;
    else if ((data[3] + data[4] + data[5]) == 3 || (data[3] + data[4] + data[5]) == 27)
        game.idPlayerWin = game.idPlayer;
    else if ((data[6] + data[7] + data[8]) == 3 || (data[6] + data[7] + data[8]) == 27)
        game.idPlayerWin = game.idPlayer;
    else if ((data[0] + data[3] + data[6]) == 3 || (data[0] + data[3] + data[6]) == 27)
        game.idPlayerWin = game.idPlayer;
    else if ((data[1] + data[4] + data[7]) == 3 || (data[1] + data[4] + data[7]) == 27)
        game.idPlayerWin = game.idPlayer;
    else if ((data[2] + data[5] + data[8]) == 3 || (data[2] + data[5] + data[8]) == 27)
        game.idPlayerWin = game.idPlayer;
    else if ((data[0] + data[4] + data[8]) == 3 || (data[0] + data[4] + data[8]) == 27)
        game.idPlayerWin = game.idPlayer;
    else if ((data[2] + data[4] + data[6]) == 3 || (data[2] + data[4] + data[6]) == 27)
        game.idPlayerWin = game.idPlayer;
    //MongoDataBase.UpdateAsync(game.idGame, game);
    return game;
});


app.Run();



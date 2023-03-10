using MongoDB.Driver;
using TicTacToe.models;

namespace TicTacToe.startup
{

    public static class MongoDataBase
    {
        public static string connectionString = "mongodb://localhost:27017/";
        public static string database = "GameTTT";
        public static string databaseCollection = "DataGame";


        private static IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(database);
            return mongoDatabase.GetCollection<T>(collection);
        }

        public static async Task<DataGame> GetAsync(DataGame dataGame)
        {
            var collection = ConnectToMongo<DataGame>(databaseCollection);
            var result = await collection.FindAsync(x => x == dataGame);
            return await result.FirstOrDefaultAsync();
        }


        public static async Task<DataGame> CreateAsync(DataGame newGame)
        {
            var collection = ConnectToMongo<DataGame>(databaseCollection);
            await collection.InsertOneAsync(newGame);
            return newGame;
        }

        public static async Task UpdateAsync(string idGame, DataGame dataGame)
        {
            var collection = ConnectToMongo<DataGame>(databaseCollection);

            await collection.ReplaceOneAsync(item => item.idGame == dataGame.idGame, dataGame);
        }

    }

}
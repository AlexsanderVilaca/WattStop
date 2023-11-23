using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Authentication;
using System.Text.Json;

namespace DataNoSQL
{
    public class MongoDB<T>
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public MongoClient Client { get; set; }
        public string Host { get; private set; }
        public string Port { get; private set; }
        public IMongoDatabase Database { get; set; }

        public MongoDB(string databaseName)
        {
            CreateClient(databaseName);
            Database = Client.GetDatabase(databaseName);
        }

        public void CreateClient(string databaseName)
        {

            string connectionUri = Settings.ConnectionURI;
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ConnectTimeout = TimeSpan.FromMinutes(5);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            Client = new MongoClient(settings);
        }

        public IMongoCollection<T> GetCollection(string collectionName)
        {
            return Database.GetCollection<T>(collectionName);
        }

        public void InsertOne(string collectionName, T document)
        {
            GetCollection(collectionName).InsertOne(document);
        }

        public void DropDatabase(string databaseName)
        {
            Client.DropDatabase(databaseName);

        }

        public string Stats()
        {
            return Database.RunCommand<BsonDocument>(new BsonDocument { { "dbstats", 1 } }).ToString();
        }
    }
}
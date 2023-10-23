using MongoDB.Bson;
using MongoDB.Driver;
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

            string host = Settings.Host;
            Host = host;
            int port = Settings.Port;
            Port = port.ToString();
            string user = Settings.User;
            string pw = Settings.Password;
            int timeoutMinutes = Settings.TimeoutMinutes;
            int timeoutSeconds = Settings.TimeoutSeconds;

            MongoClientSettings settings = new MongoClientSettings();
            settings.ConnectionMode = ConnectionMode.Standalone;
            settings.ServerSelectionTimeout = new TimeSpan(hours: 0, minutes: timeoutMinutes, seconds: timeoutSeconds);
            settings.ConnectTimeout = new TimeSpan(hours: 0, minutes: timeoutMinutes, seconds: timeoutSeconds);
            settings.SocketTimeout = new TimeSpan(hours: 0, minutes: timeoutMinutes, seconds: timeoutSeconds);
            settings.Server = new MongoServerAddress(host, port);

            if (!string.IsNullOrEmpty(user))
                settings.Credential = MongoCredential.CreateCredential(databaseName, user, pw);

            //Client = new MongoClient(ConnectionString);            
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
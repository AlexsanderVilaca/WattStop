using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL
{
    public class BaseDalNoSQL<T>
    {
        protected MongoDB<T> conexao
        {
            get
            {
                return GetConexao();
            }
        }
        protected string CollectionName { get; set; }
        protected string DataBaseName { get; set; }

        public BaseDalNoSQL(string dataBase, string collectionName)
        {
            DataBaseName = dataBase;
            CollectionName = collectionName;
        }

        public IMongoCollection<T> GetCollection()
        {
            return GetConexao().GetCollection(CollectionName);
        }
        public MongoDB<T> GetConexao()
        {
            return new MongoDB<T>(DataBaseName);
        }
        
       
        public virtual List<T> Read(Guid? id)
        {
            var collection = conexao.GetCollection(CollectionName);
            Expression<Func<T, bool>>? filtro = null;

            if (id != null)
                filtro = x => ObjectHelper.GetPropertyValue<T>(x, "Id").Equals(id);

            var lista = collection.Find(filtro).ToList();
            return lista;
        }

        public List<T> Read(FilterDefinition<T>? filter = null)
        {
            var documents = filter == null ? conexao.GetCollection(CollectionName).Find(new BsonDocument()).ToList() : conexao.GetCollection(CollectionName).Find(filter).ToList();
            return documents;
        }
        public virtual bool Delete(Guid id)
        {
            bool retorno = false;
            var collection = conexao.GetCollection(CollectionName);
            var filter = Builders<T>.Filter.Eq("_id", id);
            var retornoMongo = collection.DeleteOne(filter);
            if (retornoMongo.DeletedCount > 0)
                retorno = true;
            return retorno;
        }
        public virtual bool UpdateInsert(FilterDefinition<T> filter, T document)
        {
            return conexao.GetCollection(CollectionName).ReplaceOne(filter, document, new ReplaceOptions() { IsUpsert = true }).ModifiedCount > 0;
        }

    }
}

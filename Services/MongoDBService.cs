using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using todo_app_.net.Models;
using System.Text.Json;

namespace todo_app_.net.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Todo> _todosCollection;
    public MongoDBService(IOptions<MongoBDSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017");
        IMongoDatabase database = client.GetDatabase("Todo_dotnet");
        _todosCollection = database.GetCollection<Todo>("Todos");
    }
    public async Task CreateAsync(Todo todo)
    {
        await _todosCollection.InsertOneAsync(todo);
        return;
    }

    public async Task<List<Todo>> GetAsync()
    {
        return await _todosCollection.Find(new BsonDocument()).ToListAsync();
    }


    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Todo> filter = Builders<Todo>.Filter.Eq("Id", id);
        await _todosCollection.DeleteOneAsync(filter);
        return;
    }
}


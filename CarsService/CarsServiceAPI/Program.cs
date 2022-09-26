using MongoDB.Bson;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

const string MONGODB_URL = "mongodb://mongodb-cars-0.mongodb-cars-headless.default.svc.cluster.local:27017";

try
{
    Console.WriteLine("evo me");
    MongoClient mongoClient = new MongoClient(MONGODB_URL);
    var carsDatabase = mongoClient.GetDatabase("carsDatabase");
    var carsCollection = carsDatabase.GetCollection<BsonDocument>("cars");
    if (carsCollection == null)
    {
        Console.WriteLine("Collection doesn't exist...");
        await carsDatabase.CreateCollectionAsync("cars");
        carsCollection = carsDatabase.GetCollection<BsonDocument>("cars");

        await PopulateCarsDatabase(carsCollection);
    }
    else
    {
        var documentCount = await carsCollection.CountDocumentsAsync(new BsonDocument());

        if (documentCount == 0)
            await PopulateCarsDatabase(carsCollection);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

async Task PopulateCarsDatabase(IMongoCollection<BsonDocument> carsCollection)
{
    var documents = new List<BsonDocument>();

    var car1Object = new List<BsonElement>()
    {
        new BsonElement("carId", 0),
        new BsonElement("manufacturer", "Ford"),
        new BsonElement("model", "Focus"),
        new BsonElement("year", 2002)
    };
    var car1 = new BsonDocument(car1Object);
    var car2Object = new List<BsonElement>()
    {
        new BsonElement("carId", 0),
        new BsonElement("manufacturer", "Alfa Romeo"),
        new BsonElement("model", "GiuliaQV"),
        new BsonElement("year", 2018)
    };
    var car2 = new BsonDocument(car1Object);

    documents.Add(car1);
    documents.Add(car2);
    await carsCollection.InsertManyAsync(documents);
}

app.Run();

using Confluent.Kafka;
using MongoDB.Driver;
using Utils;

const string MONGODB_URL = "mongodb://mongodb-0.mongodb-headless.default.svc.cluster.local:27017";
string[] KAFKA_DEFECT_TOPICS = new string[2] { "batteryPercentage_topic", "overheating_topic" };

try
{
    MongoClient mongoClient = new MongoClient(MONGODB_URL);
    var defectsDatabase = mongoClient.GetDatabase("defectsDatabase");
    try
    {
        await defectsDatabase.CreateCollectionAsync("low_battery");
    }
    catch(Exception)
    {
        Console.WriteLine("Collection 'low-battery' already exists. Skiping creation...");
    }
    try
    {
        await defectsDatabase.CreateCollectionAsync("overheating");
    }
    catch (Exception)
    {
        Console.WriteLine("Collection 'overheating' already exists. Skiping creation...");
    }

    KafkaHelper.Consume(KAFKA_DEFECT_TOPICS);
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
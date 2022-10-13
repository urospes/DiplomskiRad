using Confluent.Kafka;
using MongoDB.Driver;
using Utils;

const string MONGODB_URL = "mongodb://mongodb-data-0.mongodb-data-headless.default.svc.cluster.local:27017";
string[] KAFKA_DEFECT_TOPICS = new string[5] { "batteryPercentage_topic", "overheating_topic", "engineVibrations_topic",
    "tireFailure_topic", "chassis_topic" };

try
{
    MongoClient mongoClient = new MongoClient(MONGODB_URL);
    var defectsDatabase = mongoClient.GetDatabase("defectsDatabase");
    try
    {
        await defectsDatabase.CreateCollectionAsync("defects");
    }
    catch(Exception)
    {
        Console.WriteLine("Collection 'defects' already exists. Skiping creation...");
    }

    await KafkaHelper.Consume(KAFKA_DEFECT_TOPICS, defectsDatabase);
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
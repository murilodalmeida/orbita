using MongoDB.Driver;

namespace FwksLabs.Libs.Infra.Mongo.HealthCheck.Arguments;

public sealed record MongoHealthCheckArgs(IMongoClient MongoClient);
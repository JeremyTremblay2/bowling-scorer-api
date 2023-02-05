// The port number must match the port of the gRPC server.
using BowlingGrpcClient;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7129");
var client = new Player.PlayerClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "BowlingGrpcClient" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
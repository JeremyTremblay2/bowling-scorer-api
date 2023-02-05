// The port number must match the port of the gRPC server.
using BowlingGrpcClient;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7129");
var client = new Player.PlayerClient(channel);
var getAllResult = await client.GetAllAsync(
                  new GetAllRequest { Page = 0, NbPlayers = 3 });
Console.WriteLine("Greeting: " + getAllResult.PlayerGRPC);
var getByIdResult = await client.GetByIdAsync(
                  new GetByIdRequest { Id = 1 });
Console.WriteLine("Greeting: " + getByIdResult.PlayerGRPC);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
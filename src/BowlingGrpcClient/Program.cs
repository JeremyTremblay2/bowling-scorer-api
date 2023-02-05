// The port number must match the port of the gRPC server.
using BowlingGrpcClient;
using Grpc.Net.Client;
using System.Linq.Expressions;

using var channel = GrpcChannel.ForAddress("https://localhost:7129");
var client = new Player.PlayerClient(channel);

bool run = true;

Console.WriteLine("Welcome to the Bowling GRPC Client");
while (run)
{
    Console.WriteLine("=========================");
    Console.WriteLine("Choose a request : ");
    Console.WriteLine("1- GetAllPlayers");
    Console.WriteLine("2- GetPlayerById");
    Console.WriteLine("3- AddPlayer");
    Console.WriteLine("4- EditPlayer");
    Console.WriteLine("5- DeletePlayer");
    Console.WriteLine("0- QUIT");
    Console.WriteLine("-------------------------");
    Console.WriteLine("Your choice : ");
    Int32 resp;
    try
    {
        resp = Convert.ToInt32(Console.ReadLine());
    } catch (FormatException e)
    {
        continue;
    } catch(OverflowException e)
    {
        continue;
    }
    Console.WriteLine("=========================");
    switch (resp)
    {
        case 0:
            run= false;
            break;
        case 1:
            var getAllResult = await client.GetAllAsync(
                  new GetAllRequest { Page = 0, NbPlayers = 3 });
            Console.WriteLine("GetAllResult: " + getAllResult.PlayerGRPC);
            break;
        case 2:
            var getByIdResult = await client.GetByIdAsync(
                  new GetByIdRequest { Id = 1 });
            Console.WriteLine("GetByIdResult: " + getByIdResult.PlayerGRPC);
            break;
        case 3:
            var addPlayerResult = await client.AddPlayerAsync(
                  new AddPlayerRequest { Id = 1, Name = "toto", Image = "truc.png" });
            Console.WriteLine("AddPlayerResult: " + addPlayerResult.Response);
            break;
        case 4:
            var editPlayerResult = await client.EditPlayerAsync(
                  new EditPlayerRequest { Id = 1, Name = "toto", Image = "truc.png" });
            Console.WriteLine("EditPlayerResult: " + editPlayerResult.Response);
            break;
        default:
            break;
    }
}
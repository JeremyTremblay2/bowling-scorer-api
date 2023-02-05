// The port number must match the port of the gRPC server.
using BowlingGrpcClient;
using Grpc.Net.Client;
using System.Linq.Expressions;

using var channel = GrpcChannel.ForAddress("https://localhost:7129");
var client = new PlayerGRPCService.PlayerGRPCServiceClient(channel);

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
    Console.Write("Your choice : ");
    var resp = Prompter.PromptInt();
    Console.WriteLine("=========================");
    switch (resp)
    {
        case 0:
            run= false;
            break;
        case 1:
            Console.Write("Page number : ");
            var page = Prompter.PromptInt();
            Console.Write("Number of player per page : ");
            var nbPlayers = Prompter.PromptInt();

            var getAllResult = await client.GetAllAsync(
                  new GetAllRequest { Page = page, NbPlayers = nbPlayers });
            Console.WriteLine("GetAllResult: " + getAllResult.PlayerGRPC);
            break;
        case 2:
            Console.Write("Player's id : ");
            var playerId = Prompter.PromptInt();

            var getByIdResult = await client.GetByIdAsync(
                  new GetByIdRequest { Id = playerId });
            Console.WriteLine("GetByIdResult: " + getByIdResult.PlayerGRPC);
            break;
        case 3:
            Console.Write("Player's id : ");
            var playerToAddId = Prompter.PromptInt();
            Console.Write("Player's Name : ");
            var playerToAddName = Console.ReadLine();
            Console.Write("Player's Image : ");
            var playerToAddImage = Console.ReadLine();

            var addPlayerResult = await client.AddPlayerAsync(
                  new AddPlayerRequest { Id = playerToAddId, Name = playerToAddImage, Image = playerToAddName });
            Console.WriteLine("AddPlayerResult: " + addPlayerResult.Response);
            break;
        case 4:
            Console.Write("Player's id : ");
            var playerToEditId = Prompter.PromptInt();
            Console.Write("Player's Name : ");
            var playerToEditName = Console.ReadLine();
            Console.Write("Player's Image : ");
            var playerToEditImage = Console.ReadLine();

            var editPlayerResult = await client.EditPlayerAsync(
                  new EditPlayerRequest { Id = playerToEditId, Name = playerToEditName, Image = playerToEditImage });
            Console.WriteLine("EditPlayerResult: " + editPlayerResult.Response);
            break;
        case 5:
            Console.Write("Player's id : ");
            var playerToDelId = Prompter.PromptInt();

            var delPlayerResult = await client.DeletePlayerAsync(
                  new DeletePlayerRequest { Id = playerToDelId });
            Console.WriteLine("DelPlayerResult: " + delPlayerResult.Response);
            break;
        default:
            break;
    }
}
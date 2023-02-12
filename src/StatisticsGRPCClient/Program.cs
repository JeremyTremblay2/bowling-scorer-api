using BowlingGrpcClient;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7129");
var client = new StatisticGRPCService.StatisticGRPCServiceClient(channel);

bool run = true;

Console.WriteLine("Welcome to the Statistic Bowling GRPC Client");
while (run)
{
    Console.WriteLine("=========================");
    Console.WriteLine("Choose a request : ");
    Console.WriteLine("1- GetAllStatistics");
    Console.WriteLine("2- GetStatisticById");
    Console.WriteLine("3- AddStatistic");
    Console.WriteLine("4- EditStatistic");
    Console.WriteLine("5- DeleteStatistic");
    Console.WriteLine("0- QUIT");
    Console.WriteLine("-------------------------");
    Console.Write("Your choice : ");
    var resp = Prompter.PromptInt();
    Console.WriteLine("=========================");
    switch (resp)
    {
        case 0:
            run = false;
            break;
        case 1:
            Console.Write("Page number : ");
            var page = Prompter.PromptInt();
            Console.Write("Number of statistic per page : ");
            var nbStatistic = Prompter.PromptInt();

            var getAllResult = await client.GetAllAsync(
                  new GetAllRequest { Page = page, NbStat = nbStatistic });
            Console.WriteLine("GetAllResult: " + getAllResult.StatisticGRPC);
            break;
        case 2:
            Console.Write("Statistic's id : ");
            var statisticId = Prompter.PromptInt();

            var getByIdResult = await client.GetByIdAsync(
                  new GetByIdRequest { Id = statisticId });
            Console.WriteLine("GetByIdResult: " + getByIdResult.StatisticGRPC);
            break;
        case 3:
            Console.Write("Statistic's id : ");
            var statisticToAddId = Prompter.PromptInt();
            Console.Write("Statistic's numberOfVictory : ");
            var statisticToAddNumberOfVictory = Prompter.PromptInt();
            Console.Write("Statistic's numberOfDefeat : ");
            var statisticToAddNumberOfDefeat = Prompter.PromptInt();
            Console.Write("Statistic's numberOfGames : ");
            var statisticToAddNumberOfGames = Prompter.PromptInt();
            Console.Write("Statistic's bestScore : ");
            var statisticToAddBestScore = Prompter.PromptInt();

            var addStatisticResult = await client.AddStatisticAsync(
                  new AddStatisticRequest { Id = statisticToAddId, NumberOfVictory = statisticToAddNumberOfVictory, NumberOfDefeat = statisticToAddNumberOfDefeat,
                  NumberOfGames = statisticToAddNumberOfGames, BestScore = statisticToAddBestScore});
            Console.WriteLine("AddStatisticResult: " + addStatisticResult.Response);
            break;
        case 4:
            Console.Write("Statistic's id : ");
            var statisticToEditId = Prompter.PromptInt();
            Console.Write("Statistic's numberOfVictory : ");
            var statisticToEditNumberOfVictory = Prompter.PromptInt();
            Console.Write("Statistic's numberOfDefeat : ");
            var statisticToEditNumberOfDefeat = Prompter.PromptInt();
            Console.Write("Statistic's numberOfGames : ");
            var statisticToEditNumberOfGames = Prompter.PromptInt();
            Console.Write("Statistic's bestScore : ");
            var statisticToEditBestScore = Prompter.PromptInt();

            var editStatisticResult = await client.EditStatisticAsync(
                  new EditStatisticRequest {
                      Id = statisticToEditId,
                      NumberOfVictory = statisticToEditNumberOfVictory,
                      NumberOfDefeat = statisticToEditNumberOfDefeat,
                      NumberOfGames = statisticToEditNumberOfGames,
                      BestScore = statisticToEditBestScore
                  });
            Console.WriteLine("EditStatisticResult: " + editStatisticResult.Response);
            break;
        case 5:
            Console.Write("Statistic's id : ");
            var statisticToDelId = Prompter.PromptInt();

            var delStatisticResult = await client.DeleteStatisticAsync(
                  new DeleteStatisticRequest { Id = statisticToDelId });
            Console.WriteLine("DelStatisticResult: " + delStatisticResult.Response);
            break;
        default:
            break;
    }
}

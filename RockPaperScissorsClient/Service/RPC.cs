using Grpc.Core;
using Grpc.Net.Client;
using RockPaperScissorsClient.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsClient.Service
{
    public class RPC
    {
        public static async Task BalanceCheck(string name)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7252");
            var client = new MatchService.MatchServiceClient(channel);
            try
            {
                UserData userData = await client.CheckBalanceAsync(new GetUserReq { Name = name });
                if (userData == null)
                {
                    Console.WriteLine($"Пользватель не найден.");
                }
                else
                {
                    Console.WriteLine($"Id = {userData.Id}, Name = {userData.Name}, Balance = {userData.Balance}");
                }
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Status.Detail);
            }
        }

        public static async Task MoneyTrans(string senderName, string recipientName, double money)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7252");
            var client = new MatchService.MatchServiceClient(channel);
            try
            {
                UserData userData = await client.MoneyTransAsync(new CreateTrans { SenderName = senderName, RecipientName = recipientName, AmountMoney = money });
                Console.WriteLine($"Id = {userData.Id}, Name = {userData.Name}, Balance = {userData.Balance}");
                Console.WriteLine("Деньги переведены.");
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Status.Detail);
            }
        }


        public static async Task CreateMatch(string userName, double bet, string choise)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7252");
            var client = new MatchService.MatchServiceClient(channel);
            try
            {
                MatchData matchData = await client.CreateMatchAsync(new CreateMatchRequest { CreaterName = userName, Bet = bet, CreaterHand = choise });
                Console.WriteLine($"Id = {matchData.Id}, DateTime = {matchData.DateTime}, Bet = {matchData.Bet}, CreaterName = {matchData.CreaterName}, CreaterHand = {matchData.CreaterHand}," +
                                    $" RivalId = ?, RivalHand = ?, Winner = ?, Loser = ?");
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Status.Detail);
            }
        }

        public static async Task ListMatches()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7252");
            // создаем клиент
            var client = new MatchService.MatchServiceClient(channel);
            // получение списка объектов
            ListData matches = await client.ListMatchesAsync(new Google.Protobuf.WellKnownTypes.Empty());

            foreach (var match in matches.Matches)
            {
                Console.WriteLine($"Id = {match.Id}, Date = {match.DateTime}, Bet = {match.Bet}, Creater/Player1 = {match.CreaterName}, Player1Hand = {match.CreaterHand}, " +
                    $"Player2 = {match.RivelName}, Player2Hand = {match.RivelHand}, Winner = {match.Winner}, Loser = {match.Loser}");
            }
        }

        public static async Task MatchConnect(int matchId, string userName, string choise)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7252");
            var client = new MatchService.MatchServiceClient(channel);
            MatchData match = await client.MatchConnectAsync(new RivelData { MatchId = matchId, RivelName = userName, RivelHand = choise });

            Console.WriteLine($"Id = {match.Id}, Date = {match.DateTime}, Bet = {match.Bet}, Creater/Player1 = {match.CreaterName}, Player1Hand = {match.CreaterHand}, " +
                $"Player2 = {match.RivelName}, Player2Hand = {match.RivelHand}, Winner = {match.Winner}, Loser = {match.Loser}");
            if (match.Winner == userName)
            {
                Console.WriteLine("Вы выиграли.");
            }
            else
            {
                Console.WriteLine("Вы проиграли.");
            }

        }
    }
}

using RockPaperScissorsClient.Service;

namespace RockPaperScissorsClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string input = "";

            Console.WriteLine($" 1 -> Проверить баланс.");
            Console.WriteLine($" 2 -> Денежная транзакция.");
            Console.WriteLine($" 3 -> Создать матч.");
            Console.WriteLine($" 4 -> Список матчей.");
            Console.WriteLine($" 5 -> Подключиться к матчу.");
            Console.WriteLine($" exit -> Выход.");
            Console.WriteLine($" create -> Добавить пользователя.");
            Console.WriteLine($" update -> Поменять имя пользователя.");
            Console.WriteLine($" delete -> Удалить пользователя.");

            while (input != "exit")
            {
                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "1":
                        Console.Write("Введите ваше имя: ");
                        var name = Console.ReadLine().ToLower();
                        await RPC.BalanceCheck(name);
                        break;
                    case "2":
                        Console.Write("Введите ваше имя: ");
                        var senderName = Console.ReadLine().ToLower();
                        Console.Write("Введите имя получателя: ");
                        var recepientName = Console.ReadLine();
                        Console.Write("Введите сумму перевода: ");
                        var money = double.Parse(Console.ReadLine());
                        await RPC.MoneyTrans(senderName, recepientName, money);
                        break;
                    case "3":
                        Console.Write("Введите ваше имя: ");
                        var userName = Console.ReadLine().ToLower();
                        Console.Write("Введите вашу ставку: ");
                        var bet = double.Parse(Console.ReadLine());
                        Console.Write(@"Введите ваш выбор(Rock, Paper, Sci): ");
                        var choise = Console.ReadLine().ToLower();
                        await RPC.CreateMatch(userName, bet, choise);
                        break;
                    case "4":
                        await RPC.ListMatches();
                        break;
                    case "5":
                        Console.Write("Введите id матча: ");
                        var id = int.Parse(Console.ReadLine());
                        Console.Write("Введите ваше имя: ");
                        var secondPlayer = Console.ReadLine().ToLower();
                        Console.Write(@"Введите ваш выбор(Rock, Paper, Sci): ");
                        var choise2 = Console.ReadLine().ToLower();
                        await RPC.MatchConnect(id, secondPlayer, choise2);
                        break;
                    case "create":
                        Console.Write("Введите ваше имя: ");
                        var nameUser = Console.ReadLine().ToLower();
                        await User.CreateUser(nameUser);
                        break;
                    case "update":
                        Console.Write("Введите текущее имя: ");
                        var oldName = Console.ReadLine().ToLower();
                        Console.Write("Введите новое имя: ");
                        var newName = Console.ReadLine().ToLower();
                        await User.UpdateUser(oldName, newName);
                        break;
                    case "delete":
                        Console.Write("Введите имя пользователя которого хотите удалить: ");
                        var deleteName = Console.ReadLine().ToLower();
                        await User.DeleteUser(deleteName);
                        break;
                    default:
                        Console.WriteLine("\nвыберите команду");
                        break;
                }
            }
        }
    }
}

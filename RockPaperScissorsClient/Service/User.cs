using Crud;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsClient.Service
{
    public class User
    {
        public static async Task CreateUser(string userName)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7252");
            var client = new UserService.UserServiceClient(channel);
            UserReply user = await client.CreateUserAsync(new CreateUserRequest { Name = userName });
            Console.WriteLine($"Id = {user.Id}, Name = {user.Name}, Balance = {user.Balance}");
            Console.WriteLine("Пользователь добавлен.");
        }

        public static async Task UpdateUser(string oldName, string newName)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7252");
            var client = new UserService.UserServiceClient(channel);
            try
            {
                UserReply user = await client.UpdateUserAsync(new UpdateUserRequest { OldName = oldName, NewName = newName });
                Console.WriteLine($"Id = {user.Id}, Name = {user.Name}, Balance = {user.Balance}");
                Console.WriteLine("Имя пользователя изменено.");
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Status.Detail);
            }
        }

        public static async Task DeleteUser(string deleteName)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7252");
            var client = new UserService.UserServiceClient(channel);

            try
            {
                UserReply user = await client.DeleteUserAsync(new DeleteUserRequest { Name = deleteName });
                Console.WriteLine("Пользователь удален.");
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Status.Detail);
            }

        }
    }
}

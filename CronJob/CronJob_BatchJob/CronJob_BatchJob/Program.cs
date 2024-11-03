using CronJob_BatchJob.Cart;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace CronJob_BatchJob
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");

            var connect = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(new Job_Cart(connect))
                .BuildServiceProvider();

            var cart = serviceProvider.GetService<Job_Cart>();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Tự Động Cập Nhập Đơn Hàng Khi Đợi Hơn 10 Phút");
            while (true)
            {
                cart.UpdateCartOrder();
                await Task.Delay(60000);
            }
        }
    }
}

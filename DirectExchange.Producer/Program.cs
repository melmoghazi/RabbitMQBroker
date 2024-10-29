using RabbitMQ.Client;
using System.Text;

namespace DirectExchange.Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            const string message = "Class-B";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "amq.direct",
                routingKey: "rk-new-classes",
                basicProperties: null,
                body: body);
 //           channel.Close();
            Console.WriteLine($"sent {message}");
            Console.WriteLine("Prss Enter key to close");
            Console.ReadKey();
        }
    }
}

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PoisonMessage.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            Console.WriteLine(" [*] Waiting for messages");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                //here to do some process

                //reject and requeue.
                channel.BasicNack(ea.DeliveryTag, false, true);

                Console.WriteLine($" [x] Recieved {message}");
            };


            channel.BasicConsume(queue: "q-classes", autoAck: false, consumer: consumer);

            Console.WriteLine("Prss [Enter] key to close");
            Console.ReadKey();
        }
    }
}

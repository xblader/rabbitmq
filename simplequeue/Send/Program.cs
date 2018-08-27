using RabbitMQ.Client;
using System;
using System.Text;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                
                while (true)
                {
                    i++;
                    string messagesent = $" [x] Sent {message} - {i}";
                    var body = Encoding.UTF8.GetBytes(messagesent);
                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(messagesent);
                    Console.WriteLine(" Press [enter] to exit.");
                    //Console.ReadLine();
                }
            }            
        }
    }
}

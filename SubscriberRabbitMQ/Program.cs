
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(""); //write AMQP URL

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();

    channel.QueueDeclare("queue-one", true, false, false);

    var subscriber = new EventingBasicConsumer(channel);

    channel.BasicConsume("queue-one", true, subscriber);

    subscriber.Received += (object? sender, BasicDeliverEventArgs e) =>
    {
        var message = Encoding.UTF8.GetString(e.Body.ToArray());

        Console.WriteLine($"Received Message : {message}");
    };
}
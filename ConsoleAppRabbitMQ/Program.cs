
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri(""); //write AMQP URL

string messageRead;

do
{
    Console.Write("Please write your message: ");
    messageRead = Console.ReadLine();
    if (messageRead != String.Empty && messageRead != null)
    {
        PublishMessage(messageRead);
    }
    else
    {
        Environment.Exit(0);
    }

} while (messageRead != null);

void PublishMessage(string message)
{
    try
    {
        using (var connection = factory.CreateConnection())
        {
            var channel = connection.CreateModel();

            channel.QueueDeclare("queue-one", true, false, false);

            var messageBody = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(string.Empty, "queue-one", null, messageBody);

            Console.WriteLine($"'{message}' is sended");

        }
    }
    catch (Exception ex) { Console.WriteLine(ex.ToString()); }


}



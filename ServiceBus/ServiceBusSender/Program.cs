using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusSender
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://...<enter the rest of your service bus namespace>..";
        const string QueueName = "queuesb1";
        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            const int numberOfMessages = 30;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            await SendMessageAsync(numberOfMessages);
            await queueClient.CloseAsync();
        }
        static async Task SendMessageAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    //Create a new message to sent to the queue.
                    string messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    //Write the body of the message to the console
                    Console.WriteLine($"Sending message: {messageBody}");
                    // Send the message to the queue
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }

}

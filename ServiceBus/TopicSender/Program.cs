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
        const string ServiceBusConnectionString = "Endpoint=sb://...<add the rest of your service bus namespace>...";
        const string TopicName = "sampletopic1";
        static ITopicClient topicClient;

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            const int numberOfMessages = 15;
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
            await SendMessageAsync(numberOfMessages);
            await topicClient.CloseAsync();
        }
        static async Task SendMessageAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    //Create a new message to sent to the queue.
                    string messageBody = $"Message from Leo {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                    //Write the body of the message to the console
                    Console.WriteLine($"Sending message: {messageBody}");
                    // Send the message to the queue
                    await topicClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }

}

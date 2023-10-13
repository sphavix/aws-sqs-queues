using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;
using sqspublisher.Models;


var newRegion = RegionEndpoint.GetBySystemName("us-west-2");
var sqsClient = new AmazonSQSClient(newRegion);

    var customer = new Customer()
    {
        Id = Guid.NewGuid(),
        Email = "spha.xtrader@gmail.com",
        FullName = "Spha Zolakhe",
        DateOfBirth = new DateTime(1995, 12, 28),
        GitHubUsername = "sphax"
    };

    var queueUrlResponse =  await sqsClient.GetQueueUrlAsync("customer");

    var sendMessageRequest = new SendMessageRequest
    {
        QueueUrl = queueUrlResponse.QueueUrl,
        MessageBody = JsonSerializer.Serialize(customer),
        MessageAttributes = new Dictionary<string, MessageAttributeValue>
        {
            {
                "MessageType", new MessageAttributeValue
                {
                    DataType = "String",
                    StringValue = nameof(Customer)
                }
            }
        }
    };

    var response = await sqsClient.SendMessageAsync(sendMessageRequest);

    Console.WriteLine();


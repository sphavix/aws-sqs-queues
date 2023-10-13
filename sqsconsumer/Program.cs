using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;
using sqsconsumer.Models;

var cancelToken = new CancellationTokenSource();
var newRegion = RegionEndpoint.GetBySystemName("us-west-2");
var sqsClient = new AmazonSQSClient(newRegion);


var queueUrlResponse =  await sqsClient.GetQueueUrlAsync("customer");

var receiveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl
};

while(!cancelToken.IsCancellationRequested)
{
    var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, cancelToken.Token);

    foreach(var message in response.Messages)
    {
        Console.WriteLine($"Message Id: {message.MessageId}");
        Console.WriteLine($"Message Body: {message.Body}");
    }
    await Task.Delay(3000);
}
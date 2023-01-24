using Amazon.SQS.Model;
using Amazon.SQS;
using Amazon;

namespace SQSOperation
{
    public class SQSService
    {
        private const string QueueUrl = "https://sqs.us-east-1.amazonaws.com/847888492411/aspnetb7-likhon-SQS";
        private readonly RegionEndpoint _bucketRegion = RegionEndpoint.USEast1;
        private IAmazonSQS client;

        public SQSService()
        {
            client = new AmazonSQSClient(_bucketRegion);
        }

        public async Task AddMssage(SendMessageRequest sendMessageRequest)
        {
            sendMessageRequest.QueueUrl = QueueUrl;

            var response = await client.SendMessageAsync(sendMessageRequest);
            Console.WriteLine("Sent a message with id : {0}", response.MessageId);
        }

        public async Task AddMssage(string messageBody)
        {
            var response = await client.SendMessageAsync(QueueUrl, messageBody);
            Console.WriteLine("Sent a message with id : {0}", response.MessageId);
        }

        public async Task ReadTenMessage()
        {
            for (int i = 0; i <= 10; i++)
            {
                var receiveMessageRequest = new ReceiveMessageRequest
                {
                    AttributeNames = new List<string>() { "All" },
                    MaxNumberOfMessages = 1,
                    MessageAttributeNames = { "All" },
                    QueueUrl = QueueUrl,
                    //VisibilityTimeout = (int)TimeSpan.FromMinutes(10).TotalSeconds,
                    // WaitTimeSeconds = (int)TimeSpan.FromSeconds(5).TotalSeconds
                };

                var receiveMessageResponse = await client.ReceiveMessageAsync(receiveMessageRequest);

                if (receiveMessageResponse.Messages.Count > 0)
                    Console.WriteLine("Message Body: " + receiveMessageResponse.Messages[0].Body);
            }
        }

        public async Task DeleteReadMessages()
        {
            for (int i = 0; i <= 10; i++)
            {
                var receiveMessageRequest = new ReceiveMessageRequest
                {
                    AttributeNames = new List<string>() { "All" },
                    MaxNumberOfMessages = 1,
                    QueueUrl = QueueUrl,
                    VisibilityTimeout = (int)TimeSpan.FromMinutes(10).TotalSeconds,
                    WaitTimeSeconds = (int)TimeSpan.FromSeconds(5).TotalSeconds
                };

                var receiveMessageResponse = await client.ReceiveMessageAsync(receiveMessageRequest);

                if (receiveMessageResponse.Messages.Count > 0)
                {
                    foreach (var message in receiveMessageResponse.Messages)
                    {
                        foreach (var x in message.Attributes)
                        {
                            if (x.Key == "ApproximateReceiveCount")
                            {
                                var totalRead = int.Parse(x.Value);
                                if (totalRead > 1)
                                {
                                    Console.WriteLine("Deleted Message ID '" + message.MessageId + "', Body : " + message.Body);

                                    var delRequest = new DeleteMessageRequest
                                    {
                                        QueueUrl = QueueUrl,
                                        ReceiptHandle = message.ReceiptHandle
                                    };

                                    var delResponse = await client.DeleteMessageAsync(delRequest);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

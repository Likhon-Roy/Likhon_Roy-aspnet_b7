using Amazon.SQS.Model;
using SQSOperation;

var sqs = new SQSService();

var sendMessageRequest = new SendMessageRequest
{
    DelaySeconds = 10,
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
        {
            { "Title", new MessageAttributeValue { DataType = "String", StringValue = "The Whistler" } },
            { "Author", new MessageAttributeValue { DataType = "String", StringValue = "John Grisham" } },
            { "WeeksOn", new MessageAttributeValue { DataType = "Number", StringValue = "6" } }
        },
    MessageBody = "Information about current NY Times fiction bestseller for week of 12/11/2016.",
};

await sqs.AddMssage(sendMessageRequest);
await sqs.AddMssage("Add message only with body");

await sqs.ReadTenMessage();

await sqs.DeleteReadMessages();
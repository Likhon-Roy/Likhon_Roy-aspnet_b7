using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace DynamoDb
{
    public class DynamoDbService
    {
        private readonly AmazonDynamoDBClient client;
        private readonly Table table;
        private string tableName = "aspnetb7-likhon";

        public DynamoDbService()
        {
            client = new AmazonDynamoDBClient(RegionEndpoint.USEast1);
            table = Table.LoadTable(client, tableName);
        }

        public async Task CreateBookItem(Document book)
        {
            Console.WriteLine("\n*** Executing CreateBookItem() ***");

            await table.PutItemAsync(book);
        }

        public async Task<Dictionary<string, AttributeValue>> GetBookById(int bookId, int price)
        {
            Console.WriteLine("\n*** Executing RetrieveBook() ***");

            var key = new Dictionary<string, AttributeValue>
            {
                {"Id", new AttributeValue { N = bookId.ToString() }},
                {"Price", new AttributeValue { N = price.ToString() }},
            };

            var request = new GetItemRequest
            {
                TableName = tableName,
                Key = key,
                ConsistentRead = true
            };

            GetItemResponse response = await client.GetItemAsync(request);

            PrintItem(response.Item);

            return response.Item;
        }

        public async Task DeleteBook(int bookId, int Price)
        {
            Console.WriteLine("\n*** Executing DeleteBook() ***");

            var key = new Dictionary<string, AttributeValue>
            {
                {"Id", new AttributeValue { N = bookId.ToString() }},
                {"Price", new AttributeValue { N = Price.ToString() }},
            };

            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = key
            };

            await client.DeleteItemAsync(request);

            Console.WriteLine("DeleteBook operation successful");
        }

        public async Task GetTenDataByQuery()
        {
            var queryRequest = new QueryRequest
            {
                TableName = tableName,
                KeyConditionExpression = "Id > :v_Id",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {":v_Id", new AttributeValue {N =  "60"}}},
                Limit = 10

            };
            QueryResponse queryResponse = await client.QueryAsync(queryRequest);
        }

        public async Task<List<Dictionary<string, AttributeValue>>> GetTenDataByScan()
        {
            var forumScanRequest = new ScanRequest
            {
                TableName = tableName,
                FilterExpression = "Price > :val",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                        {":val", new AttributeValue { N = "50" }}
                },
                // ProjectionExpression = "Id, Title",
                Limit = 10
            };

            var response = await client.ScanAsync(forumScanRequest);

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                PrintItem(item);
            }

            return response.Items;
        }

        private void PrintDocument(Document updatedDocument)
        {
            foreach (var attribute in updatedDocument.GetAttributeNames())
            {
                string stringValue = null;
                var value = updatedDocument[attribute];
                if (value is Primitive)
                    stringValue = value.AsPrimitive().Value.ToString();
                else if (value is PrimitiveList)
                    stringValue = string.Join(",", (from primitive
                                    in value.AsPrimitiveList().Entries
                                                    select primitive.Value).ToArray());
                Console.WriteLine("{0} - {1}", attribute, stringValue);
            }
        }

        private static void PrintItem(Dictionary<string, AttributeValue> attributeList)
        {
            foreach (KeyValuePair<string, AttributeValue> kvp in attributeList)
            {
                string attributeName = kvp.Key;
                AttributeValue value = kvp.Value;

                Console.WriteLine(
                    attributeName + " " +
                    (value.S == null ? "" : "S=[" + value.S + "]") +
                    (value.N == null ? "" : "N=[" + value.N + "]") +
                    (value.SS == null ? "" : "SS=[" + string.Join(",", value.SS.ToArray()) + "]") +
                    (value.NS == null ? "" : "NS=[" + string.Join(",", value.NS.ToArray()) + "]")
                    );
            }
            Console.WriteLine("************************************************");
        }
    }
}

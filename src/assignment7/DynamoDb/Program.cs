using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using DynamoDb;

try
{
    var dynamoDb = new DynamoDbService();

    int i = 1;

    // for (i = 1; i <= 15; i++)
    // {
    var book = new Document();
    book["Id"] = 10 * i;
    book["Title"] = "Book ";
    book["Price"] = 12 * i;
    book["ISBN"] = "111-1111111111";
    book["Authors"] = new List<string> { "Author 1", "Author 2", "Author 3" };
    book["PageCount"] = 500;
    book["Dimensions"] = "8.5x11x.5";
    book["InPublication"] = new DynamoDBBool(true);
    book["InStock"] = new DynamoDBBool(false);
    book["QuantityOnHand"] = 0;
    book["CreationTime"] = DateTime.Now;

    await dynamoDb.CreateBookItem(book);
    // }

    var partitionKey = 10;
    var sortKey = 12;
    var book1 = await dynamoDb.GetBookById(partitionKey, sortKey);

    var books = await dynamoDb.GetTenDataByScan();

    await dynamoDb.DeleteBook(partitionKey, sortKey);
}
catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
catch (Exception e) { Console.WriteLine(e.Message); }

using S3Bucket;

var s3Bucket = new S3BucketService();

var keyName = "myfile";
var filePath = "C:\\Users\\LikhonR\\Downloads\\images (1).jpg";
await s3Bucket.UploadFile(keyName, filePath);


// var downloadPath = "C:\\Users\\LikhonR\\Downloads\\myfile1542.jpg";
// var downloadFilekeyName = "myfile";
// await s3Bucket.DownloadFile(downloadFilekeyName, downloadPath);


// await s3Bucket.DeleteFile(keyName);
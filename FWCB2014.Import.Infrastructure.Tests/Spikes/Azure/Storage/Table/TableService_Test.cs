using FWCB2014.Import.Infrastructure.Tests.Properties;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NUnit.Framework;

namespace FWCB2014.Import.Infrastructure.Tests.Spikes.Azure.Storage.Table
{
  [TestFixture]
  public class TableService_Test
  {
    [Test]
    [Explicit]
    public void Create()
    {
      const string tableName = "global";

      var storageAccount = CloudStorageAccount.Parse(Settings.Default.StorageConnectionString);

      var tableClient = storageAccount.CreateCloudTableClient();

      var table = tableClient.GetTableReference(tableName);
      table.CreateIfNotExists();

      var player = new PlayerEntity
      {
        PartitionKey = "players",
        RowKey = "mattia.richetto@gmail.com",
        FirstName = "mattia",
        LastName = "richetto",
        ShirtNumber = 14
      };

      var insertOperation = TableOperation.Insert(player);

      table.Execute(insertOperation);
    }

    [Test]
    [Explicit]
    public void Read()
    {
      // todo: create App.config

      const string tableName = "global";

      var storageAccount = CloudStorageAccount.Parse(Settings.Default.StorageConnectionString);

      var tableClient = storageAccount.CreateCloudTableClient();
      var table = tableClient.GetTableReference(tableName);

      //table.CreateIfNotExists();

      //var player = new PlayerEntity
      //{
      //    PartitionKey = "players",
      //    RowKey = "mattia.richetto@gmail.com",
      //    FirstName = "mattia",
      //    LastName = "richetto",
      //    ShirtNumber = 14
      //};

      //var insertOperation = TableOperation.Insert(player);

      //table.Execute(insertOperation);

      var retrieveOperation = TableOperation.Retrieve<PlayerEntity>("players", "mattia.richetto@gmail.com");

      var retrievedResult = table.Execute(retrieveOperation);

      // see: http://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-table-storage-20/#create-table
    }

  }

  public class PlayerEntity : TableEntity
  {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int ShirtNumber { get; set; }
  }
}

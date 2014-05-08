using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Infrastructure.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FWCB2014.Domain.Infrastructure.Repositories
{
  public class AzureCountryRepository : CountryRepositoryBase
  {
    private readonly string _connectionString;
    private readonly string _tableName;
    private readonly string _partitionKey;
    private readonly string _rowKey;

    private IEnumerable<CountryModel> _countries;
    protected override IEnumerable<CountryModel> Countries
    {
      get
      {
        if (_countries == null)
        {
          var storageAccount = CloudStorageAccount.Parse(_connectionString);
          var tableClient = storageAccount.CreateCloudTableClient();
          var table = tableClient.GetTableReference(_tableName);
          
          var retrieveOperation = TableOperation.Retrieve<DataEntity>(_partitionKey, _rowKey);
          var retrievedResult = table.Execute(retrieveOperation);

          var countries = JsonConvert.DeserializeObject<IEnumerable<CountryModel>>(((DataEntity)retrievedResult.Result).Data);

          _countries = countries;
        }
        return _countries;
      }
    }

    public override void Add(IEnumerable<CountryModel> models)
    {
      var storageAccount = CloudStorageAccount.Parse(_connectionString);
      var tableClient = storageAccount.CreateCloudTableClient();
      var table = tableClient.GetTableReference(_tableName);

      table.CreateIfNotExists();

      var data = new DataEntity { PartitionKey = _partitionKey, RowKey = _rowKey, Data = JsonConvert.SerializeObject(models) };

      var insertOrReplaceOperation = TableOperation.InsertOrReplace(data);
      table.Execute(insertOrReplaceOperation);
    }

    public override void Delete()
    {
      var storageAccount = CloudStorageAccount.Parse(_connectionString);
      var tableClient = storageAccount.CreateCloudTableClient();
      var table = tableClient.GetTableReference(_tableName);

      var retrieveOperation = TableOperation.Retrieve<DataEntity>(_partitionKey, _rowKey);
      var retrievedResult = table.Execute(retrieveOperation);

      var deleteEntity = (DataEntity)retrievedResult.Result;

      if (deleteEntity == null) return;

      var deleteOperation = TableOperation.Delete(deleteEntity);
      table.Execute(deleteOperation);
    }

    public AzureCountryRepository(string connectionString, string tableName, string partitionKey, string rowKey)
    {
      _connectionString = connectionString;
      _tableName = tableName;
      _partitionKey = partitionKey;
      _rowKey = rowKey;
    }
  }
}
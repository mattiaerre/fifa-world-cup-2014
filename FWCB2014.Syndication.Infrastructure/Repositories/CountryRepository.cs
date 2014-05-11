using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Infrastructure.Entities;
using FWCB2014.Syndication.Core.Repositories;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FWCB2014.Syndication.Infrastructure.Repositories
{
  public class CountryRepository : ICountryRepository
  {
    private readonly string _connectionString;
    private readonly string _tableName;
    private readonly string _partitionKey;
    private readonly string _rowKey;

    private IEnumerable<CountryModel> _countries;
    private IEnumerable<CountryModel> Countries
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

    public CountryRepository(string connectionString, string tableName, string partitionKey, string rowKey)
    {
      _connectionString = connectionString;
      _tableName = tableName;
      _partitionKey = partitionKey;
      _rowKey = rowKey;
    }

    public IEnumerable<CountryModel> Find(Func<CountryModel, bool> predicate)
    {
      var countries = Countries.Where(predicate);
      return countries;
    }
  }
}

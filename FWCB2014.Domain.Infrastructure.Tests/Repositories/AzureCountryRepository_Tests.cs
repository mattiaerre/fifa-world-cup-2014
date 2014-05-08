using System.Collections.Generic;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;
using FWCB2014.Domain.Infrastructure.Entities;
using FWCB2014.Domain.Infrastructure.Repositories;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FWCB2014.Domain.Infrastructure.Tests.Repositories
{
  [TestFixture]
  [Explicit]
  public class AzureCountryRepository_Tests
  {
    private IRepository<CountryModel> _repository;

    private const string ConnectionString = "UseDevelopmentStorage=true";
    private const string TableName = "countries";
    private const string PartitionKey = "wc";
    private const string RowKey = "2014";

    [SetUp]
    public void Given_An_AzureCountryRepository()
    {
      _repository = new AzureCountryRepository(ConnectionString, TableName, PartitionKey, RowKey);
    }

    [Test]
    public void It_Should_Be_Able_To_Add_Countries()
    {
      var data = new DataEntity { PartitionKey = PartitionKey, RowKey = RowKey };
      data.Data = JsonConvert.SerializeObject(new { Countries = true });
      _repository.Add(new List<CountryModel>
      {
        new CountryModel { Name = "Bermuda", Alpha2Code = "BM", Alpha3Code = "BMU", },
        new CountryModel { Name = "Denmark", Alpha2Code = "DK", Alpha3Code = "DNK", },
      });
    }

    [TearDown]
    public void Delete_All()
    {
      _repository.Delete();
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Infrastructure.Entities;
using FWCB2014.Domain.Infrastructure.Repositories;
using FWCB2014.Domain.Infrastructure.Tests.Properties;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;

namespace FWCB2014.Domain.Infrastructure.Tests.Repositories
{
  [Obsolete("every context will use its own implementation of the repositories", true)]
  [TestFixture]
  [Explicit]
  public class CountryRepository_Tests
  {
    [TestCase("Italy")]
    public void It_Should_Return_A_Country_Given_Its_Name(string name)
    {
      var jsonPath = string.Format("{0}{1}", Settings.Default.SyndicationRoot, @"\App_Data\Countries.json");
      var fileRepository = new FileCountryRepository(jsonPath);
      var fileCountry = fileRepository.Find(e => e.Name == name).First();

      var azureRepository = new AzureCountryRepository(Settings.Default.StorageConnectionString, "countries", Settings.Default.CompetitionId, Settings.Default.SeasonId);
      var azureCountry = azureRepository.Find(e => e.Name == name).First();

      Assert.AreEqual(fileCountry.Name, azureCountry.Name);
      Assert.AreEqual(fileCountry.Alpha2Code, azureCountry.Alpha2Code);
      Assert.AreEqual(fileCountry.Alpha3Code, azureCountry.Alpha3Code);
    }

    [Test]
    public void Add_Countries_To_Azure_Storage()
    {
      var jsonPath = @"D:\Users\mattia\Source\Repos\fifa-world-cup-2014\FWCB2014.Syndication.Web\App_Data\Countries.json";

      var countriesJson = File.ReadAllText(jsonPath);

      var storageAccount = CloudStorageAccount.Parse(Settings.Default.StorageConnectionString);

      var tableClient = storageAccount.CreateCloudTableClient();

      var table = tableClient.GetTableReference("countries");
      table.CreateIfNotExists();

      var entity = new DataEntity
      {
        PartitionKey = Settings.Default.CompetitionId,
        RowKey = Settings.Default.SeasonId,
        Data = countriesJson,
      };

      var insertOperation = TableOperation.Insert(entity);

      table.Execute(insertOperation);
    }

    [Test]
    public void Read_Countries_From_Azure_Storage()
    {
      var storageAccount = CloudStorageAccount.Parse(Settings.Default.StorageConnectionString);

      var tableClient = storageAccount.CreateCloudTableClient();

      var table = tableClient.GetTableReference("countries");

      var retrieveOperation = TableOperation.Retrieve<DataEntity>(Settings.Default.CompetitionId, Settings.Default.SeasonId);

      var retrievedResult = table.Execute(retrieveOperation);

      var countries = JsonConvert.DeserializeObject<IEnumerable<CountryModel>>(((DataEntity)retrievedResult.Result).Data);
    }
  }
}

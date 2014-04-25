﻿using System;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;

namespace FWCB2014.Import.Core.Services
{
  public class CountriesService : ICountriesService
  {
    private readonly IRepository<CountryModel> _repository;

    public CountriesService(IRepository<CountryModel> repository)
    {
      _repository = repository;
    }

    public TeamModelBase GetCountry(string countryCode)
    {
      throw new NotImplementedException();
    }
  }
}
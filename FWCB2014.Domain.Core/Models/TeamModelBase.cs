﻿namespace FWCB2014.Domain.Core.Models
{
  // todo: this is actually a country model
  public class TeamModelBase : ICode // info: try to make it abstract
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public string Alpha2Code { get; set; }
    public string Alpha3Code { get; set; }
  }
}
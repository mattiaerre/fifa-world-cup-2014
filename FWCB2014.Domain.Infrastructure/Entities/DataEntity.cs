using Microsoft.WindowsAzure.Storage.Table;

namespace FWCB2014.Domain.Infrastructure.Entities
{
  public class DataEntity : TableEntity
  {
    public string Data { get; set; }
  }
}
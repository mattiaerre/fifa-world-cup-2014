using System.Collections.Generic;
using FWCB2014.Domain.Core.Models.Groups;

namespace FWCB2014.Import.Core.Services
{
  public interface IGroupsService
  {
    IEnumerable<Group> GetAll();
  }
}
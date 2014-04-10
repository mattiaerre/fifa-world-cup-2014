using FWCB2014.Domain.Core.Models.Command.Groups;

namespace FWCB2014.Domain.Core.Models.Query.Groups
{
  public class TeamModel : TeamModelBase, ICountryModel
  {
    public CountryModel Country { get; set; }
  }
}

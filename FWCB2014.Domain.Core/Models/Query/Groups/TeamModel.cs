namespace FWCB2014.Domain.Core.Models.Query.Groups
{
    public class TeamModel : CountryModel, ITeamId<string>
    {
        public string TeamId { get; set; }
    }
}
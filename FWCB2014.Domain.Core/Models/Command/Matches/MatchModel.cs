using System;

namespace FWCB2014.Domain.Core.Models.Command.Matches
{
  //<item contest="wc_2014" id="497e52f0581dea4efcd45f1dcd2d1556" status="not_started" timestamp-starts="1402603200">
  //  <teams>
  //    <hosts id="bra_int">
  //      <name>Brazil</name>
  //      <fullname>Brazil</fullname>
  //    </hosts>
  //    <guests id="cro_int">
  //      <name>Croatia</name>
  //      <fullname>Croatia</fullname>
  //    </guests>
  //  </teams>
  //  <score>0 - 0</score>
  //  <details>
  //    <contest>
  //      <competition id="wc_2014">
  //        <title>wc_2014</title>
  //      </competition>
  //      <season>2014</season>
  //    </contest>
  //    <fixture-info>1</fixture-info>
  //    <group-id>A</group-id>
  //  </details>
  //</item>

  public class MatchModel : MatchModelBase, ICompetitionCode, ISeasonCode, IId<string>, IStatus<MatchStatus>, IDate, IGroupId<string>
  {
    public string CompetitionCode { get; set; }
    public string SeasonCode { get; set; }
    public string Id { get; set; }
    public MatchStatus Status { get; set; }
    public DateTime Date { get; set; }
    public string GroupId { get; set; }
    public TeamModel HomeTeam { get; set; }
    public TeamModel AwayTeam { get; set; }
    public string FixtureInfo { get; set; }
  }
}

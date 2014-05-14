using System;
using System.Collections.Generic;
using FWCB2014.Domain.Core.Models.Command.Matches;

namespace FWCB2014.Import.Core.Models.Matches
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

	// see: http://msdn.microsoft.com/en-us/library/ff506346(v=vs.110).aspx

	public class MatchModel : IId<string>, IStatus<MatchStatus>
	{
	  private MatchEventThrower _thrower;
	  public MatchEventThrower Thrower {
	    set
	    {
	      _thrower = value;
        _thrower.ThrowEvent += _thrower_ThrowEvent;
	    }
	  }

    void _thrower_ThrowEvent(object sender, EventArgs args)
    {
      Minute = ((MatchEventModel) args).Minute;
    }

	  public string Id { get; private set; }
		public MatchStatus Status { get; private set; }
		public DateTime DateTime { get; private set; }
    public TeamModel HomeTeam { get; private set; }
    public TeamModel AwayTeam { get; private set; }
    public MatchPeriod Period { get; private set; }
    public int Minute { get; private set; }
    public IList<MatchEventModel> Events { get; private set; }

	  public MatchModel(string id, DateTime dateTime, string homeTeamId, string awayTeamId)
		{
		  Id = id;
			Status = MatchStatus.NotStarted;
			DateTime = dateTime;
		  HomeTeam = new TeamModel(homeTeamId);
	    AwayTeam = new TeamModel(awayTeamId);
	    Events = new List<MatchEventModel>();
		}
	}

  public class MatchEventThrower
  {
    public delegate void EventHandler(object sender, EventArgs args);

    public event EventHandler ThrowEvent = delegate { };

    public void SomethingHappened(EventArgs args)
    {
      ThrowEvent(this, args);
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



  public class MatchModel : ICode, ICompetitionCode, IStatus<MatchStatus>, IDate
  {
    public string Code { get; set; }
    public string CompetitionCode { get; set; }
    public MatchStatus Status { get; set; }
    public DateTime Date { get; set; }
  }
}

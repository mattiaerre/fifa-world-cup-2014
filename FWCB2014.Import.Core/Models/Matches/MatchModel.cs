using System;

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

	public class MatchModel : IId<string>, IStatus<MatchStatus>, IObserver<MatchMessage>
	{
		private IDisposable _unsubscriber;

		public string Id { get; private set; }
		public MatchStatus Status { get; private set; }
		public DateTime DateTime { get; private set; }

		public MatchModel(string id, DateTime dateTime)
		{
			Id = id;
			Status = MatchStatus.NotStarted;
			DateTime = dateTime;
		}

		public void OnNext(MatchMessage value)
		{
			throw new NotImplementedException();
		}

		public void OnError(Exception error)
		{
			throw new NotImplementedException();
		}

		public void OnCompleted()
		{
			throw new NotImplementedException();
		}

		public virtual void Subscribe(IObservable<MatchMessage> provider)
		{
			_unsubscriber = provider.Subscribe(this);
		}

		public virtual void Unsubscribe()
		{
			_unsubscriber.Dispose();
		}
	}
}
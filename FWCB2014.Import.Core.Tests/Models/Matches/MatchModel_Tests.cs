using System;
using FWCB2014.Import.Core.Extensions;
using FWCB2014.Import.Core.Models.Matches;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FWCB2014.Import.Core.Tests.Models.Matches
{
	[TestFixture]
	public class MatchModel_Tests
	{
		private MatchModel _match;
		private const string MatchId = "G815";
		private readonly DateTime _matchDateTime = Convert.ToDouble("1402603200").ToUtc();

		[SetUp]
		public void Given_A_MatchModel()
		{
			_match = new MatchModel(MatchId, _matchDateTime);
		}

		[Test]
		public void It_Should_Have_An_Unique_Identifier()
		{
			Assert.IsNotNull(_match.Id);
		}

		[Test]
		public void It_Should_Be_NotStarted()
		{
			Assert.AreEqual(MatchStatus.NotStarted, _match.Status);
		}

		[Test]
		public void It_Should_Have_A_DateTime()
		{
			Assert.AreEqual(_matchDateTime, _match.DateTime);
		}

		[Test]
		public void It_Should_Be_Able_To_Change_Its_Status_Into_Active()
		{
			// see: http://msdn.microsoft.com/en-us/library/ee850490(v=vs.110).aspx
		}

		[Test]
		public void Serialize_Deserialize()
		{
			var json = JsonConvert.SerializeObject(_match);

			var match = JsonConvert.DeserializeObject<MatchModel>(json);

			Assert.AreEqual(_match.Id, match.Id);
			Assert.AreEqual(_match.Status, match.Status);
			Assert.AreEqual(_match.DateTime, match.DateTime);
		}
	}
}

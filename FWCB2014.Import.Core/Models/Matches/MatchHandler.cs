using System;
using System.Collections.Generic;

namespace FWCB2014.Import.Core.Models.Matches
{
	// http://msdn.microsoft.com/en-us/library/ee850490(v=vs.110).aspx
	public class MatchHandler : IObservable<MatchMessage>
	{
		private readonly IList<IObserver<MatchMessage>> _observers;
		private readonly IList<MatchMessage> _messages;

		public MatchHandler()
		{
			_observers = new List<IObserver<MatchMessage>>();
			_messages = new List<MatchMessage>();
		}

		public IDisposable Subscribe(IObserver<MatchMessage> observer)
		{
			if (!_observers.Contains(observer))
			{
				_observers.Add(observer);
				foreach (var item in _messages)
					observer.OnNext(item);
			}
			return new Unsubscriber<MatchMessage>(_observers, observer);
		}

		private class Unsubscriber<T> : IDisposable
		{
			private readonly IList<IObserver<T>> _observers;
			private readonly IObserver<T> _observer;

			internal Unsubscriber(IList<IObserver<T>> observers, IObserver<T> observer)
			{
				_observers = observers;
				_observer = observer;
			}

			public void Dispose()
			{
				if (_observers.Contains(_observer))
					_observers.Remove(_observer);
			}
		}
	}
}
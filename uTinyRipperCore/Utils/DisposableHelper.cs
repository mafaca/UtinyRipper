using System;

namespace uTinyRipper
{
	/// <summary>
	/// A helper that calls an action once when it is disposed
	/// For use with <c>using</c> constructs
	/// </summary>
	public class DisposableHelper : IDisposable
	{
		public DisposableHelper(Action action)
		{
			m_action = action;
		}

		public void Dispose()
		{
			m_action?.Invoke();
			m_action = null;
		}

		private Action m_action;
	}
}
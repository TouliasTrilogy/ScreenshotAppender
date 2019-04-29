using System;
using System.Collections.Generic;
using System.Runtime;
using System.Threading;

namespace ScreenshotAppender
{
	/// <summary>
	/// Implement list with changed event when content of list changed
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class NotifyList<T> : List<T>
	{
		private Mutex _mutex;
		public event EventHandler<EventArgs> OnChange;

		[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
		public NotifyList(Mutex mutex = null) : base()
		{
			_mutex = mutex;
		}

		[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
		public NotifyList(int capacity, Mutex mutex = null) : base(capacity)
		{
			_mutex = mutex;
		}

		public NotifyList(IEnumerable<T> collection, Mutex mutex = null) : base(collection)
		{
			_mutex = mutex;
		}

		public new void Add(T value)
		{
			_mutex?.WaitOne();
			base.Add(value);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void AddRange(IEnumerable<T> collection)
		{
			_mutex?.WaitOne();
			base.AddRange(collection);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void Clear()
		{
			_mutex?.WaitOne();
			base.Clear();
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void Insert(int index, T value)
		{
			_mutex?.WaitOne();
			base.Insert(index, value);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void InsertRange(int index, IEnumerable<T> collection)
		{
			_mutex?.WaitOne();
			base.InsertRange(index, collection);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void Remove(T value)
		{
			_mutex?.WaitOne();
			base.Remove(value);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void RemoveAt(int index)
		{
			_mutex?.WaitOne();
			base.RemoveAt(index);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void RemoveAll(Predicate<T> match)
		{
			_mutex?.WaitOne();
			base.RemoveAll(match);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void RemoveRange(int index, int count)
		{
			_mutex?.WaitOne();
			base.RemoveRange(index, count);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void Reverse(int index, int count)
		{
			_mutex?.WaitOne();
			base.Reverse(index, count);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void Reverse()
		{
			_mutex?.WaitOne();
			base.Reverse();
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void Sort(int index, int count, IComparer<T> comparer)
		{
			_mutex?.WaitOne();
			base.Sort(index, count, comparer);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void Sort(Comparison<T> comparison)
		{
			_mutex?.WaitOne();
			base.Sort(comparison);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}
		public new void Sort()
		{
			_mutex?.WaitOne();
			base.Sort();
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void Sort(IComparer<T> comparer)
		{
			_mutex?.WaitOne();
			base.Sort(comparer);
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}

		public new void TrimExcess()
		{
			_mutex?.WaitOne();
			base.TrimExcess();
			OnChange?.Invoke(this, new EventArgs());
			_mutex?.ReleaseMutex();
		}
	}
}

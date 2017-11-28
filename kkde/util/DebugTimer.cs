using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace kkde.util
{
	public static class DebugTimer
	{
		[ThreadStatic]
		static Stopwatch stopWatch;

		/// <summary>
		/// 時間計測を開始する
		/// </summary>
		[Conditional("DEBUG")]
		public static void Start()
		{
			if (stopWatch == null)
			{
				stopWatch = new Stopwatch();
			}
			stopWatch.Start();
		}

		/// <summary>
		/// 時間計測をストップしデバッグ出力する
		/// </summary>
		/// <param name="desc">表示する文字列</param>
		[Conditional("DEBUG")]
		public static void Stop(string desc)
		{
			stopWatch.Stop();
			Debug.WriteLine(String.Format("{0}: {1} ms", desc, stopWatch.ElapsedMilliseconds));
			stopWatch.Reset();
		}
	}
}

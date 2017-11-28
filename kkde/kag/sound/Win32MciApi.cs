using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace kkde.kag.sound
{
	/// <summary>
	/// MCI関連クラス
	/// </summary>
	public static class Win32MciApi
	{
		/// <summary>
		/// エイリアス名（KKDEユニークな物）
		/// </summary>
		public const string MCI_ALIAS_FILE_NAME = "KKDE_SOUND_FILE";

		/// <summary>
		/// ウィンドウメッセージ
		/// </summary>
		public const int MM_MCINOTIFY = 0x3B9;

		/* flags for wParam of MM_MCINOTIFY message */
		public const int MCI_NOTIFY_SUCCESSFUL = 0x0001;
		public const int MCI_NOTIFY_SUPERSEDED = 0x0002;
		public const int MCI_NOTIFY_ABORTED = 0x0004;
		public const int MCI_NOTIFY_FAILURE = 0x0008;

		/* common flags for dwFlags parameter of MCI command messages */
		public const int MCI_NOTIFY = 0x00000001;
		public const int MCI_WAIT = 0x00000002;
		public const int MCI_FROM = 0x00000004;
		public const int MCI_TO = 0x00000008;
		public const int MCI_TRACK = 0x00000010;

		/* MCI command message identifiers */
		public const int MCI_OPEN = 0x0803;
		public const int MCI_CLOSE = 0x0804;
		public const int MCI_ESCAPE = 0x0805;
		public const int MCI_PLAY = 0x0806;
		public const int MCI_SEEK = 0x0807;
		public const int MCI_STOP = 0x0808;
		public const int MCI_PAUSE = 0x0809;
		public const int MCI_INFO = 0x080A;
		public const int MCI_GETDEVCAPS = 0x080B;
		public const int MCI_SPIN = 0x080C;
		public const int MCI_SET = 0x080D;
		public const int MCI_STEP = 0x080E;
		public const int MCI_RECORD = 0x080F;
		public const int MCI_SYSINFO = 0x0810;
		public const int MCI_BREAK = 0x0811;
		public const int MCI_SAVE = 0x0813;
		public const int MCI_STATUS = 0x0814;
		public const int MCI_CUE = 0x0830;
		public const int MCI_REALIZE = 0x0840;
		public const int MCI_WINDOW = 0x0841;
		public const int MCI_PUT = 0x0842;
		public const int MCI_WHERE = 0x0843;
		public const int MCI_FREEZE = 0x0844;
		public const int MCI_UNFREEZE = 0x0845;
		public const int MCI_LOAD = 0x0850;
		public const int MCI_CUT = 0x0851;
		public const int MCI_COPY = 0x0852;
		public const int MCI_PASTE = 0x0853;
		public const int MCI_UPDATE = 0x0854;
		public const int MCI_RESUME = 0x0855;
		public const int MCI_DELETE = 0x0856;

		/* flags for dwFlags parameter of MCI_OPEN command message */
		public const int MCI_OPEN_SHAREABLE = 0x00000100;
		public const int MCI_OPEN_ELEMENT = 0x00000200;
		public const int MCI_OPEN_ALIAS = 0x00000400;
		public const int MCI_OPEN_ELEMENT_ID = 0x00000800;
		public const int MCI_OPEN_TYPE_ID = 0x00001000;
		public const int MCI_OPEN_TYPE = 0x00002000;

		[StructLayout(LayoutKind.Explicit, Size = 20)]
		public struct MCI_OPEN_PARMS
		{
			[FieldOffset(0)]
			public uint dwCallback;
			[FieldOffset(4)]
			public uint wDeviceID;
			[FieldOffset(8)]
			public string lpstrDeviceType;
			[FieldOffset(12)]
			public string lpstrElementName;
			[FieldOffset(16)]
			public string lpstrAlias;
		}

		/// <summary>
		/// MCIコマンドを文字列で発行する
		/// </summary>
		/// <param name="command">コマンド文字列</param>
		/// <param name="buffer"></param>
		/// <param name="bufferSize"></param>
		/// <param name="hwndCallback"></param>
		/// <returns>成功したときは0</returns>
		[DllImport("winmm.dll", CharSet = CharSet.Auto)]
		public static extern int mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
	}
}

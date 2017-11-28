using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace kkde.kag.sound
{
	/// <summary>
	/// Win32APIでPCM関連のAPI取得関数
	/// </summary>
	public static class Win32PcmApi
	{
		public delegate void waveOutProc(IntPtr hwo, uint uMsg, uint dwInstance, uint dwParam1, uint dwParam2);

		#region ウィンドウメッセージ
		public const int MM_WOM_OPEN = 0x3BB;           /* waveform output */
		public const int MM_WOM_CLOSE = 0x3BC;
		public const int MM_WOM_DONE = 0x3BD;

		public const int WOM_OPEN = MM_WOM_OPEN;
		public const int WOM_CLOSE = MM_WOM_CLOSE;
		public const int WOM_DONE = MM_WOM_DONE;
		#endregion

		#region 定義

		// Can be used instead of a device id to open a device
		public const uint WAVE_MAPPER = unchecked((uint)(-1));

		// Flag specifying the use of a callback window for sound messages
		public const uint CALLBACK_TYPEMASK = 0x00070000;    /* callback type mask */
		public const uint CALLBACK_NULL = 0x00000000;    /* no callback */
		public const uint CALLBACK_WINDOW = 0x00010000;    /* dwCallback is a HWND */
		public const uint CALLBACK_TASK = 0x00020000;    /* dwCallback is a HTASK */
		public const uint CALLBACK_FUNCTION = 0x00030000;    /* dwCallback is a FARPROC */

		// Error information...
		private const int WAVERR_BASE = 32;
		private const int MMSYSERR_BASE = 0;

		// Enum equivalent to MMSYSERR_*
		public enum MMSYSERR : int
		{
			NOERROR = 0,
			ERROR = (MMSYSERR_BASE + 1),
			BADDEVICEID = (MMSYSERR_BASE + 2),
			NOTENABLED = (MMSYSERR_BASE + 3),
			ALLOCATED = (MMSYSERR_BASE + 4),
			INVALHANDLE = (MMSYSERR_BASE + 5),
			NODRIVER = (MMSYSERR_BASE + 6),
			NOMEM = (MMSYSERR_BASE + 7),
			NOTSUPPORTED = (MMSYSERR_BASE + 8),
			BADERRNUM = (MMSYSERR_BASE + 9),
			INVALFLAG = (MMSYSERR_BASE + 10),
			INVALPARAM = (MMSYSERR_BASE + 11),
			HANDLEBUSY = (MMSYSERR_BASE + 12),
			INVALIDALIAS = (MMSYSERR_BASE + 13),
			BADDB = (MMSYSERR_BASE + 14),
			KEYNOTFOUND = (MMSYSERR_BASE + 15),
			READERROR = (MMSYSERR_BASE + 16),
			WRITEERROR = (MMSYSERR_BASE + 17),
			DELETEERROR = (MMSYSERR_BASE + 18),
			VALNOTFOUND = (MMSYSERR_BASE + 19),
			NODRIVERCB = (MMSYSERR_BASE + 20),
			LASTERROR = (MMSYSERR_BASE + 20)
		}

		// Enum equivalent to WAVERR_*
		private enum WAVERR : int
		{
			NONE = 0,
			BADFORMAT = WAVERR_BASE + 0,
			STILLPLAYING = WAVERR_BASE + 1,
			UNPREPARED = WAVERR_BASE + 2,
			SYNC = WAVERR_BASE + 3,
			LASTERROR = WAVERR_BASE + 3
		}

		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// Invalid format
		/// </summary>
		public const uint WAVE_INVALIDFORMAT = 0x00000000;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 11.025 kHz, Mono,   8-bit
		/// </summary>
		public const uint WAVE_FORMAT_1M08 = 0x00000001;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 11.025 kHz, Stereo, 8-bit
		/// </summary>
		public const uint WAVE_FORMAT_1S08 = 0x00000002;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 11.025 kHz, Mono,   16-bit
		/// </summary>
		public const uint WAVE_FORMAT_1M16 = 0x00000004;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 11.025 kHz, Stereo, 16-bit
		/// </summary>
		public const uint WAVE_FORMAT_1S16 = 0x00000008;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 22.05  kHz, Mono,   8-bit
		/// </summary>
		public const uint WAVE_FORMAT_2M08 = 0x00000010;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 22.05  kHz, Stereo, 8-bit
		/// </summary>
		public const uint WAVE_FORMAT_2S08 = 0x00000020;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 22.05  kHz, Mono,   16-bit
		/// </summary>
		public const uint WAVE_FORMAT_2M16 = 0x00000040;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 22.05  kHz, Stereo, 16-bit
		/// </summary>
		public const uint WAVE_FORMAT_2S16 = 0x00000080;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 44.1   kHz, Mono,   8-bit
		/// </summary>
		public const uint WAVE_FORMAT_4M08 = 0x00000100;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 44.1   kHz, Stereo, 8-bit
		/// </summary>
		public const uint WAVE_FORMAT_4S08 = 0x00000200;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 44.1   kHz, Mono,   16-bit
		/// </summary>
		public const uint WAVE_FORMAT_4M16 = 0x00000400;
		/// <summary>
		/// Used by dwFormats in WAVEINCAPS and WAVEOUTCAPS
		/// 44.1   kHz, Stereo, 16-bit
		/// </summary>
		public const uint WAVE_FORMAT_4S16 = 0x00000800;

		// Accessors specifying data positions in a .wave file
		// RIFF header up to 20 bytes in .wav file
		public const int WF_OFFSET_FORMATTAG = 20;
		public const int WF_OFFSET_CHANNELS = 22;
		public const int WF_OFFSET_SAMPLESPERSEC = 24;
		public const int WF_OFFSET_AVGBYTESPERSEC = 28;
		public const int WF_OFFSET_BLOCKALIGN = 32;
		public const int WF_OFFSET_BITSPERSAMPLE = 34;
		// Offset 2 for wBitsPerSample
		// + 4 for the subchunk id "data"
		// + 4 for the subchunk length
		public const int WF_OFFSET_DATA = 44;


		[StructLayout(LayoutKind.Explicit, Size = 18)]
		public struct WAVEFORMATEX
		{
			[FieldOffset(0)]
			public ushort wFormatTag;

			[FieldOffset(2)]
			public ushort nChannels;

			[FieldOffset(4)]
			public uint nSamplesPerSec;

			[FieldOffset(8)]
			public uint nAvgBytesPerSec;

			[FieldOffset(12)]
			public ushort nBlockAlign;

			[FieldOffset(14)]
			public ushort wBitsPerSample;

			[FieldOffset(16)]
			public ushort cbSize;
		}

		[StructLayout(LayoutKind.Explicit, Size = 32)]
		public struct WAVEHDR_TAG
		{
			[FieldOffset(0)]
			public IntPtr lpData;

			[FieldOffset(4)]
			public uint dwBufferLength;

			[FieldOffset(8)]
			public uint dwBytesRecorded;

			[FieldOffset(12)]
			public uint dwUser;

			[FieldOffset(16)]
			public uint dwFlags;

			[FieldOffset(20)]
			public uint dwLoops;

			[FieldOffset(24)]
			public IntPtr lpNext;

			[FieldOffset(28)]
			public uint reserved;
		}
		#endregion

		#region 関数
		[DllImport("winmm.dll", EntryPoint = "waveOutOpen", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern unsafe uint waveOutOpen(ref IntPtr phwo, uint uDeviceID, ref WAVEFORMATEX pwfx, uint dwCallback, uint dwInstance, uint fdwOpen);

		[DllImport("winmm.dll", EntryPoint = "waveOutPrepareHeader", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern unsafe uint waveOutPrepareHeader(IntPtr hwo, ref WAVEHDR_TAG pwh, uint cbwh);

		[DllImport("winmm.dll", EntryPoint = "waveOutWrite", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern unsafe uint waveOutWrite(IntPtr hwo, ref WAVEHDR_TAG pwh, uint cbwh);

		[DllImport("winmm.dll", EntryPoint = "waveOutClose", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern unsafe uint waveOutClose(IntPtr hwo);

		[DllImport("winmm.dll", EntryPoint = "waveOutReset", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern unsafe uint waveOutReset(IntPtr hwo);

		[DllImport("winmm.dll", EntryPoint = "waveOutUnprepareHeader", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern unsafe uint waveOutUnprepareHeader(IntPtr hwo, ref WAVEHDR_TAG pwh, uint cbwh);
		#endregion
	}
}
